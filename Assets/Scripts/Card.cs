using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Sprite buildingImage;

    public int goldCostAmount, gemCostAmount;

    [SerializeField] private TextMeshProUGUI requiredGoldSourceText;
    [SerializeField] private TextMeshProUGUI requiredGemSourceText;
    void Start()
    {
        this.gameObject.transform.Find("CardImage").GetComponent<Image>().sprite = buildingImage;
        requiredGemSourceText.text = gemCostAmount.ToString();
        requiredGoldSourceText.text = goldCostAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStatsAboutCardOnUI()
    {
        this.gameObject.transform.Find("CardImage").GetComponent<Image>().sprite = buildingImage;
        requiredGemSourceText.text = gemCostAmount.ToString();
        requiredGoldSourceText.text = goldCostAmount.ToString();
    }
}
