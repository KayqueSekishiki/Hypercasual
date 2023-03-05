using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPerson : MonoBehaviour
{
    public GameObject playerPrefab;
    public PlayerController playerController;
    public SOPlayer soPlayer;
   // private GameObject _currentPlayer;

    public void SelectPlayer()
    {
        //playerController.playerPrefab = playerPrefab;
        if (soPlayer.currentPlayer != null) Destroy(soPlayer.currentPlayer);
        soPlayer.currentPlayer = Instantiate(playerPrefab, playerController.transform);
    }
}
