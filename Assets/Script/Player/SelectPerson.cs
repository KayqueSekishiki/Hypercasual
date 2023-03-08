using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectPerson : MonoBehaviour
{
    public GameObject playerPrefab;
    public PlayerController playerController;
    public SOPlayer soPlayer;

    [Header("Animations")]
    public float scaleDuration = .5f;
    public float scaleFactor = 1f;
    public Ease ease = Ease.Linear;

    public void SelectPlayer()
    {
        if (soPlayer.currentPlayer != null) Destroy(soPlayer.currentPlayer);

        playerPrefab.transform.localScale = Vector3.zero;
        soPlayer.currentPlayer = Instantiate(playerPrefab, playerController.transform);
        soPlayer.currentPlayer.transform.DOScale(scaleFactor, scaleDuration).SetEase(ease);

    }
}
