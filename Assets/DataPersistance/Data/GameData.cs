using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int playerGemSource;
    public int playerGoldSource;
    public int goldCostAmountOfCard;
    public int gemCostAmountOfCard;
    public int constructionTime;
    public bool isThereEnoughResources;

    public GameData()
    {
        playerGemSource = 10;
        playerGoldSource = 10;
    }
}
