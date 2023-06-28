using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour, IDataPersistance
{
    public int gemSource = 10;
    public int goldSource = 10;
 
    public static PlayerResources Instance;

    public delegate void PlayerResourceTextUpdate();
    public event PlayerResourceTextUpdate UpdateThePlayerResourceText;

    public delegate void IsResourcesEnoughtToBuilding();
    public event IsResourcesEnoughtToBuilding isCurrentResourceEnoughForCardCost;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }

    public void DecreasePlayerSource(int costOfGold, int costOfGem)
    {
        gemSource -= costOfGem;
        goldSource -= costOfGold;
        UpdateThePlayerResourceText.Invoke();
        isCurrentResourceEnoughForCardCost.Invoke();
    }

    public void IncreasePlayerSource(int increaseAmountOfGold, int increaseAmountOfGem)
    {
        gemSource += increaseAmountOfGem;
        goldSource += increaseAmountOfGold;
        UpdateThePlayerResourceText.Invoke();
        isCurrentResourceEnoughForCardCost.Invoke();
    }

    public void LoadData(GameData data)
    {
        this.gemSource = data.playerGemSource;
        this.goldSource = data.playerGoldSource;
    }

    public void SaveData(ref GameData data)
    {
        data.playerGemSource = this.gemSource;
        data.playerGoldSource = this.goldSource;
    }
}
