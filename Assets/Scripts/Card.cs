using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite buildingImage;

    [SerializeField] private Vector2[] blockPiecePositionsToCreateShape;

    public int goldCostAmount, gemCostAmount;

    public int constructionTime;

    private bool isConstructionFinished;

    public GameObject blockShapePrefab;

    [SerializeField] private int gemResourceGainAmount;
    [SerializeField] private int goldResourceGainAmount;

    [SerializeField] private int resourceGeneratorCooldownAmount;

    private bool mouseClicked;

    private Color currentDefaultColor;

    private bool isThereEnoughResource;

    [SerializeField] private TextMeshProUGUI requiredGoldSourceText;
    [SerializeField] private TextMeshProUGUI requiredGemSourceText;

    void Start()
    {
        isThereEnoughResource = true;
        currentDefaultColor = this.gameObject.GetComponent<Image>().color;
        this.gameObject.transform.Find("CardImage").GetComponent<Image>().sprite = buildingImage;
        requiredGemSourceText.text = gemCostAmount.ToString();
        requiredGoldSourceText.text = goldCostAmount.ToString();
        PlayerResources.Instance.isCurrentResourceEnoughForCardCost += CurrentResourceIsEnoughForThisCard;
    }

    public void UpdateStatsAboutCardOnUI()
    {
        this.gameObject.transform.Find("CardImage").GetComponent<Image>().sprite = buildingImage;
        requiredGemSourceText.text = gemCostAmount.ToString();
        requiredGoldSourceText.text = goldCostAmount.ToString();
    }

    public void CurrentResourceIsEnoughForThisCard()
    {
        if (PlayerResources.Instance.goldSource < goldCostAmount || PlayerResources.Instance.gemSource < gemCostAmount)
        {
            this.gameObject.GetComponent<Image>().raycastTarget = false;
            this.gameObject.GetComponent<Image>().maskable = false;
            this.gameObject.GetComponent<Image>().color = Color.red;
        }

        if (PlayerResources.Instance.goldSource >= goldCostAmount && PlayerResources.Instance.gemSource >= gemCostAmount)
        {
            this.gameObject.GetComponent<Image>().raycastTarget = true;
            this.gameObject.GetComponent<Image>().maskable = true;
            this.gameObject.GetComponent<Image>().color = currentDefaultColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isThereEnoughResource)
        {
            var blockShape = Instantiate(blockShapePrefab, transform.position, Quaternion.identity);
            blockShape.GetComponent<ShapeBlock>().goldCost = goldCostAmount;
            blockShape.GetComponent<ShapeBlock>().gemCost = gemCostAmount;
            blockShape.GetComponent<ShapeBlock>().constructionTime = constructionTime;
            blockShape.GetComponent<ShapeBlock>().buildingSprite = buildingImage;
            blockShape.GetComponent<ShapeBlock>().gemResourceGainAmount = gemResourceGainAmount;
            blockShape.GetComponent<ShapeBlock>().goldResourceGainAmount = goldResourceGainAmount;
            blockShape.GetComponent<ShapeBlock>().resourceGenerateCooldown = resourceGeneratorCooldownAmount;
            blockShape.transform.SetParent(transform);
            blockShape.GetComponent<ShapeBlock>().blockPiecePositions = new Vector2[blockPiecePositionsToCreateShape.Length];
            for (int i = 0; i < blockPiecePositionsToCreateShape.Length; i++)
            {
                blockShape.GetComponent<ShapeBlock>().blockPiecePositions[i] = blockPiecePositionsToCreateShape[i];
            }
            if (blockShape != null)
            {
                blockShape.GetComponent<ShapeBlockSelector>().SelectBlock();
            }
        }
    }
}
