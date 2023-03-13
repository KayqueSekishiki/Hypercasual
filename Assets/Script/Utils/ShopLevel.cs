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

    [Header("Audio")]
    public AudioSource audioSource;
    public ButtonAudioAccept buttonAudioAccept;
    private List<AudioClip> _audioClipList;




    private void Start()
    {
        LevelPrice();
        UpdadeLevelsOpenned();
        _audioClipList = buttonAudioAccept.audioClipList;
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
            audioSource.clip = _audioClipList[2];
            audioSource.Play();
            BuyErrorMenu.SetActive(true);
            errorMessageText.text = "Level " + (levelIndex - 1) + " not yet unlocked.";
            return;
        }

        if (currentCoins < LevelPrices[levelIndex])
        {

            audioSource.clip = _audioClipList[2];
            audioSource.Play();
            BuyErrorMenu.SetActive(true);
            errorMessageText.text = "Insufficient Coins!";
            return;
        }

        audioSource.clip = _audioClipList[1];
        audioSource.Play();
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
