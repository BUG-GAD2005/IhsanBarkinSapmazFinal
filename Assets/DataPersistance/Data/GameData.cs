using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameData 
{
    public int playerGemSource;
    public int playerGoldSource;

    public int constructionTime;

    public Sprite buildingSprite;

    public int gemCost;
    public int goldCost;

    public int gemResourceGainAmount;
    public int goldResourceGainAmount;

    public int resourceGenerateCooldown;

    public Transform parentPos;
    public GameData()
    {
        playerGemSource = 10;
        playerGoldSource = 10;
    }
}

