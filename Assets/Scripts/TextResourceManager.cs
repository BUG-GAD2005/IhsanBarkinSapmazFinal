using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TextResourceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textGem;
    [SerializeField] private TextMeshProUGUI textGold;
    void Start()
    {
        PlayerResources.Instance.UpdateThePlayerResourceText += UpdateResourceTexts;
    }

    public void UpdateResourceTexts()
    {
        textGem.text = PlayerResources.Instance.gemSource.ToString();
        textGold.text = PlayerResources.Instance.goldSource.ToString();
    }
}
