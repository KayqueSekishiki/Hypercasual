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


        public void PauseGame()
    {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0 : 1;
        pauseMenu.SetActive(_isPaused);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PauseGame();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        PauseGame();
    }


 
}
