using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class FloatinGainedResourceText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gemText;
    [SerializeField] private TextMeshProUGUI goldText;
    public int gainedGoldAmount;
    public int gainedGemAmount;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }
    public void ShowTheAmountOfGainedResource()
    {
        gemText.text = gainedGemAmount.ToString();
        goldText.text = gainedGoldAmount.ToString();
    }

    
}
