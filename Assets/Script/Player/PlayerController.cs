using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

public class PlayerController : Singleton<PlayerController>
{
    //publics
    [Header("Lerp Config")]
    public Transform target;
    public float lerpSpeed = 1f;


    [Header("Player")]
    public TextMeshProUGUI playerStatus;
    public float initialSpeed = 1f;

    public GameObject endScreen;

    public GameObject coinCollector;

    //privates
    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private bool _invincible;


    private void Start()
    {
        _currentSpeed = initialSpeed;
        ResetStatusName();
    }


    void Update()
    {
        if (!_canRun) return;
        _pos = target.position;
        _pos.y = target.position.y;
        _pos.z = transform.position.z;


        transform.Translate(_currentSpeed * Time.deltaTime * transform.forward);
        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy") && _invincible)
        {
            Destroy(collision.gameObject);
        }
        else if (collision.transform.CompareTag("Enemy") && !_invincible)
        {
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Endline"))
        {
            EndGame();
        }
    }


    private void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
    }

    public void StartToRun()
    {
        _canRun = true;
    }

    #region POWERUPS

    public void PowerUpSpeedUp(string statusName, float addSpeed)
    {
        playerStatus.text = statusName;
        _currentSpeed += addSpeed;
    }

    public void PowerUpSpeedUpEnd()
    {
        ResetStatusName();
        _currentSpeed = initialSpeed;
    }

    public void PowerUpInvincible(string statusName)
    {
        playerStatus.text = statusName;
        _invincible = true;
    }

    public void PowerUpInvincibleEnd()
    {
        ResetStatusName();
        _invincible = false;
    }

    public void PowerUpFly(string statusName, float amount)
    {
        playerStatus.text = statusName;
        var pos = target.position;
        target.position = new Vector3(target.position.x, pos.y += amount, target.position.z);
    }

    public void PowerUpFlyEnd(float amount)
    {
        ResetStatusName();
        var pos = target.position;
        target.position = new Vector3(target.position.x, pos.y -= amount, target.position.z);
    }

    public void PowerUpGetCoins(string statusName, float amount)
    {
        playerStatus.text = statusName;
        coinCollector.transform.localScale = Vector3.one * amount;

    }

    public void PowerUpGetCoinsEnd()
    {
        ResetStatusName();
        coinCollector.transform.localScale = Vector3.one;
    }

    public void ResetStatusName()
    {
        playerStatus.text = "";
    }



    #endregion
}




