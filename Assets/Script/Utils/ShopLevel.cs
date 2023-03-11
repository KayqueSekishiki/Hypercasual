using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopLevel : MonoBehaviour
{
    public List<GameObject> buttonsList;
    public List<int> LevelPrices;
    public int levelPriceFactor;

    public TextMeshProUGUI buyThisLevelText;
    public TextMeshProUGUI yourCoinsText;

    public GameObject buyLevelMenu;
    public GameObject buyLevelConfirmButton;


    private void Start()
    {
        LevelPrice();
    }

    public void LevelPrice()
    {
        for (int i = 0; i < buttonsList.Count; i++)
        {
            LevelPrices.Add(i * levelPriceFactor);
        }
    }

    public void OpenBuyMenu(int levelIndex)
    {
        buyLevelMenu.SetActive(true);
        TextPrice(levelIndex);
        AddOnclickButton(levelIndex);
        UpdateYourCoinsText();
    }

    public void BuyLevel(int levelIndex)
    {
        int currentCoins = PlayerPrefs.GetInt("_coinsKey");
        if (currentCoins < LevelPrices[levelIndex]) return;
        currentCoins -= LevelPrices[levelIndex];
        buttonsList[levelIndex].SetActive(false);
        buyLevelMenu.SetActive(false);

        PlayerPrefs.SetInt("_coinsKey", currentCoins);
    }

    public void TextPrice(int levelIndex)
    {
        buyThisLevelText.text = "Do you really want to buy this level for " + LevelPrices[levelIndex] + " coins?";
    }

    private void UpdateYourCoinsText()
    {
        int currentCoins = PlayerPrefs.GetInt("_coinsKey");
        yourCoinsText.text = "Your Coins: " + currentCoins;
    }

    private void AddOnclickButton(int levelIndex)
    {
        var button = buyLevelConfirmButton.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { BuyLevel(levelIndex); });
    }

    public void aa()
    {

    }
}
