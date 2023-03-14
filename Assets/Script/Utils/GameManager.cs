using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ebac.Core.Singleton;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [Header("PauseMenu")]
    public GameObject pauseMenu;
    private bool _isPaused = false;

    [Header("Camera Control")]
    public GameObject camera1;
    public GameObject camera2;


    public void PauseGame()
    {
        _isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void UnpauseGame()
    {
        _isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        UnpauseGame();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        UnpauseGame();
    }



    public void ChangeCamera()
    {
        camera2.SetActive(!camera2.activeSelf);
        camera1.SetActive(!camera1.activeSelf);
    }

}
