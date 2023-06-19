using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Sprite buildingImage;

    [SerializeField] private Vector2[] blockPiecePositionsToCreateShape;

    public int goldCostAmount, gemCostAmount;

    public int constructionTime;

    private bool isConstructionFinished;


    [SerializeField] private TextMeshProUGUI requiredGoldSourceText;
    [SerializeField] private TextMeshProUGUI requiredGemSourceText;
    void Start()
    {
        this.gameObject.transform.Find("CardImage").GetComponent<Image>().sprite = buildingImage;
        requiredGemSourceText.text = gemCostAmount.ToString();
        requiredGoldSourceText.text = goldCostAmount.ToString();
    }

    public void UpdateStatsAboutCardOnUI()
    {
        this.gameObject.transform.Find("CardImage").GetComponent<Image>().sprite = buildingImage;
        requiredGemSourceText.text = gemCostAmount.ToString();
        requiredGoldSourceText.text = goldCostAmount.ToString();
    }
}
