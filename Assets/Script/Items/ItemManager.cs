using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    private int _playerTotalCoins;
    private string _coinsKey = "_coinsKey";

    public TextMeshProUGUI uiTextTotalCoins;


    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
    }

    public void AddCoins(int amount)
    {
        coins.value += amount;
        UIInGameManager.Instance.UpdateTextCoins(coins.value);
    }

    public void SaveCoinsData()
    {
        _playerTotalCoins = PlayerPrefs.GetInt(_coinsKey);
        _playerTotalCoins += coins.value;
        PlayerPrefs.SetInt(_coinsKey, _playerTotalCoins);
    }

    public void UpdateTotalCoins()
    {
        uiTextTotalCoins.text = "Your Total Coins: " + PlayerPrefs.GetInt(_coinsKey);

        Debug.Log("PlayerPrefsCoins: " + PlayerPrefs.GetInt(_coinsKey));
    }
}
