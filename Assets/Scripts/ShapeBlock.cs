using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
public class ShapeBlock : MonoBehaviour
{
    [SerializeField] private GameObject blockPiecePrefab;
    public Vector2[] blockPiecePositions;
    private ShapeBlockPiece[] blockPieces = new ShapeBlockPiece[5];

    [SerializeField] Image constructionTimerImage;

    [SerializeField]
    [Tooltip("Pieces will set their size to grid cell size automatically in game.")]
    private float previewPieceSize = 40, previewPieceOffset = 3f;

    [HideInInspector] public ShapeBlockSelector selector;

    public int constructionTime;

    public Sprite buildingSprite;

    public int gemCost;
    public int goldCost;

    public int gemResourceGainAmount;
    public int goldResourceGainAmount;

    public int resourceGenerateCooldownAmount;
    private bool isConstructed;
    private void Start()
    {
        selector = GetComponent<ShapeBlockSelector>();

        SpawnBlockPieces();
    }

    public void UpdateBlockPiecesPreview()
    {
        SpawnBlockPieces();
    }

    private void SpawnBlockPieces()
    {
        for (var n = transform.childCount - 1; n >= 0; n--)
        {
            DestroyImmediate(transform.GetChild(n).gameObject, false);
        }

        blockPieces = new ShapeBlockPiece[blockPiecePositions.Length];

        for (var n = 0; n < blockPiecePositions.Length; n++)
        {
            Vector2 blockPiecePos = blockPiecePositions[n];

            Vector2 cellPos = CustomGridGenerator.GetCellPos((int)blockPiecePos.x, (int)blockPiecePos.y, previewPieceSize, previewPieceOffset);

            Transform piece = Instantiate(blockPiecePrefab, transform).transform;
            piece.localPosition = cellPos;

            ShapeBlockPiece blockPiece = piece.GetComponent<ShapeBlockPiece>();
            blockPiece.ownerBlock = this;

            blockPieces[n] = blockPiece;
        }

    }

    public void PlaceBlock()
    {
        for (var n = 0; n < blockPieces.Length; n++)
        {
            Vector2 blockPiecePos = blockPiecePositions[n];
            ShapeBlockPiece blockPiece = blockPieces[n];

            int targetCell = CalculateTargetCell(blockPiecePos);

            blockPiece.transform.SetParent(transform.parent, true);
            blockPiece.transform.position = GridManager.Instance.GetGridCellPos(targetCell);


            blockPiece.ownerBlock = null;

            GridManager.Instance.GetCell(targetCell).Occupy(true);

        }

        PlayerResources.Instance.DecreasePlayerSource(goldCost,gemCost);

        StartCoroutine(ConstructionTimer());
        //Destroy(gameObject);
    }

    public bool CanPlace()
    {
        int pivotPieceColumn = PlayerController.Instance.HoveredCell / 10;

        foreach (Vector2 blockPiecePos in blockPiecePositions)
        {
            int targetCell = CalculateTargetCell(blockPiecePos);

            bool isCellEmpty = GridManager.Instance.IsCellEmpty(targetCell);

            bool isColumnSlipped = targetCell / 10 != pivotPieceColumn + blockPiecePos.x && blockPiecePos.y > 0;

            if (!isCellEmpty || isColumnSlipped) return false;
        }

        return true;
    }

    private int CalculateTargetCell(Vector2 pos)
    {
        int x = (int)pos.x * GridManager.Instance.indexPerColumn;
        int y = (int)pos.y * GridManager.Instance.indexPerRow;

        int currentCell = PlayerController.Instance.HoveredCell;
        int targetCell = currentCell + x + y;

        return targetCell;
    }

    private void ConstructionState(Image constructionTimerImage)
    {
        constructionTimerImage.fillAmount = (constructionTime*10) / 100f;
    }

    private void ResourceGeneratingState(Image resourceGeneratingTimerImage)
    {
        resourceGeneratingTimerImage.fillAmount = (resourceGenerateCooldownAmount * 10) / 100f;
    }

    IEnumerator ConstructionTimer()
    {
        var timerImage = Instantiate(constructionTimerImage,transform.position, Quaternion.identity);
        timerImage.transform.SetParent(blockPieces[0].transform);
        timerImage.transform.position = blockPieces[0].transform.position;
        var tempTimer = constructionTime;

        foreach (var item in blockPieces)
        {
            item.gameObject.GetComponent<Image>().color = Color.white;
            item.gameObject.GetComponent<Image>().raycastTarget = false;
            item.gameObject.GetComponent<Image>().maskable = false;
        }

        for (int i = tempTimer; tempTimer >= 0; tempTimer--)
        {
            constructionTime--;
            ConstructionState(timerImage);
            Debug.Log("TEST01");
            yield return new WaitForSeconds(1f);
        }

        foreach (var item in blockPieces)
        {
            item.GetComponent<Image>().sprite = buildingSprite;
        }

        StartCoroutine(BuildingResourceGenerateState());
    }

    IEnumerator BuildingResourceGenerateState()
    {
        for (int i = 0; i < 20000; i++)
        {
            var timerImage = Instantiate(constructionTimerImage, transform.position, Quaternion.identity);
            timerImage.transform.SetParent(blockPieces[0].transform);
            timerImage.transform.position = blockPieces[0].transform.position;
            var tempTimer = resourceGenerateCooldownAmount;
            for (int a = tempTimer; tempTimer >= 0; tempTimer--)
            {
                resourceGenerateCooldownAmount--;
                ResourceGeneratingState(timerImage);
                yield return new WaitForSeconds(1f);
            }
            Destroy(timerImage);
            Debug.Log("RESOURCE AMOUNT IS ADDING");
            PlayerResources.Instance.IncreasePlayerSource(goldResourceGainAmount, gemResourceGainAmount);
            Debug.Log("RESOURCE GENERATOR FINISHED");
        }


    }
}
