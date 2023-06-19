using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ShapeBlock : MonoBehaviour
{
    [SerializeField] private GameObject blockPiecePrefab;
    public Vector2[] blockPiecePositions;
    private ShapeBlockPiece[] blockPieces;

    [SerializeField]
    [Tooltip("Pieces will set their size to grid cell size automatically in game.")]
    private float previewPieceSize = 40, previewPieceOffset = 3f;

    [HideInInspector] public ShapeBlockSelector selector;

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

        Destroy(gameObject);
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
}
