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
    [Header("ERROR MENU")]
    public GameObject BuyErrorMenu;
    public TextMeshProUGUI errorMessageText;



    private void Start()
    {
        LevelPrice();
        UpdadeLevelsOpenned();
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
        BuyErrorMenu.SetActive(false);
        TextPrice(levelIndex);
        AddOnclickButton(levelIndex);
        UpdateYourCoinsText();
    }

    public void BuyLevel(int levelIndex)
    {
        int currentCoins = PlayerPrefs.GetInt("_coinsKey");

        if (buttonsList[levelIndex - 1].activeInHierarchy == true)
        {
            BuyErrorMenu.SetActive(true);
            errorMessageText.text = "Level " + (levelIndex - 1) + " not yet unlocked.";
            return;
        }

        if (currentCoins < LevelPrices[levelIndex])
        {
            BuyErrorMenu.SetActive(true);
            errorMessageText.text = "Insufficient Coins!";
            return;
        }
        currentCoins -= LevelPrices[levelIndex];
        buttonsList[levelIndex].SetActive(false);
        buyLevelMenu.SetActive(false);

        PlayerPrefs.SetInt("_coinsKey", currentCoins);
        PlayerPrefs.SetInt("totalLevelsOpenned", 1 + levelIndex);
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

    public void UpdadeLevelsOpenned()
    {
        int levelsOpenned = PlayerPrefs.GetInt("totalLevelsOpenned");

        for (int i = 0; i < levelsOpenned; i++)
        {
            buttonsList[i].SetActive(false);
        }
    }
}
