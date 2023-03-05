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
    public GameObject defaultPlayerPrefab;
    public SOPlayer soPlayer;

    public GameObject endScreen;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;
    private bool _win = false;
    private bool _lose = false;



    [HideInInspector] public bool canRun;
    //privates
    private Vector3 _pos;
    private float _currentSpeed;
    private bool _invincible;



    private void Start()
    {
        if (soPlayer.currentPlayer == null)
        {
            soPlayer.currentPlayer = Instantiate(defaultPlayerPrefab, transform);
            animatorManager = soPlayer.currentPlayer.GetComponent<AnimatorManager>();
            animatorManager.Play(AnimatorManager.AnimationType.IDLE);
        }

        playerStatus.text = "";
        _currentSpeed = initialSpeed;
    }


    void Update()
    {
        if (!canRun) return;
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
            EndGame(AnimatorManager.AnimationType.DEAD);
            _lose = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Endline"))
        {
            EndGame(AnimatorManager.AnimationType.WIN);
            _win = true;
        }
    }


    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
    }

    public void StartToRun()
    {
        canRun = true;
        animatorManager = soPlayer.currentPlayer.GetComponent<AnimatorManager>();
        animatorManager.Play(AnimatorManager.AnimationType.RUN);
    }

    #region POWERUPS

    public void PowerUpSpeedUp(string statusName, float addSpeed)
    {
        playerStatus.text = statusName;
        _currentSpeed += addSpeed;
        animatorManager.Play(AnimatorManager.AnimationType.SPRINT);
    }

    public void PowerUpSpeedUpEnd(string statusName)
    {
        ResetStatusName(statusName);
        _currentSpeed = initialSpeed;

        if (_win)
        {
            animatorManager.Play(AnimatorManager.AnimationType.WIN);
        }
        else if (_lose)
        {
            animatorManager.Play(AnimatorManager.AnimationType.STAYDEAD);

        }
        else
        {
            animatorManager.Play(AnimatorManager.AnimationType.RUN);
        }
    }

    public void PowerUpInvincible(string statusName)
    {
        playerStatus.text = statusName;
        _invincible = true;
    }

    public void PowerUpInvincibleEnd(string statusName)
    {
        ResetStatusName(statusName);
        _invincible = false;
    }

    public void PowerUpFly(string statusName, float amount)
    {
        playerStatus.text = statusName;
        var pos = target.position;
        target.position = new Vector3(target.position.x, pos.y += amount, target.position.z);
        animatorManager.Play(AnimatorManager.AnimationType.FLYING);
    }

    public void PowerUpFlyEnd(string statusName, float amount)
    {
        var pos = target.position;
        target.position = new Vector3(target.position.x, pos.y -= amount, target.position.z);
        animatorManager.Play(AnimatorManager.AnimationType.RUN);
        ResetStatusName(statusName);
    }

    public void PowerUpGetCoins(string statusName, float amount)
    {
        playerStatus.text = statusName;
        coinCollector.transform.localScale = Vector3.one * amount;

    }

    public void PowerUpGetCoinsEnd(string statusName)
    {
        ResetStatusName(statusName);
        coinCollector.transform.localScale = Vector3.one;
    }

    public void ResetStatusName(string statusName)
    {
        if (playerStatus.text != statusName)
        {
            return;
        }
        else
        {
            playerStatus.text = "";
        }
    }



    #endregion
}




