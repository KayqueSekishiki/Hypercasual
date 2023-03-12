using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Ebac.Core.Singleton;

public class PlayerController : Singleton<PlayerController>
{
    //publics
    [Header("Lerp Config")]
    public Transform target;
    public float lerpSpeed = 1f;


    [Header("Player")]
    public Canvas playerCanvas;
    public TextMeshProUGUI playerStatus;
    public float initialSpeed = 1f;
    public GameObject defaultPlayerPrefab;
    public SOPlayer soPlayer;

    public GameObject endScreen;
    [Header("Jump Raycast")]
    public float distToGround = 1f;


    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;
    public BounceHelper bounceHelper;
    private bool _win = false;
    private bool _lose = false;

    [Header("Animations DGTweening")]
    public float scaleDuration = .05f;
    public float scaleFactor = 1f;
    public Ease ease = Ease.Linear;

    [HideInInspector] public bool canRun = false;
    //privates
    private Vector3 _pos;
    private float _currentSpeed;
    private float _baseSpeedToAnimation = 15f;
    private bool _invincible;




    private void Start()
    {
        if (soPlayer.currentPlayer == null)
        {
            defaultPlayerPrefab.transform.localScale = Vector3.zero;
            soPlayer.currentPlayer = Instantiate(defaultPlayerPrefab, transform);
            soPlayer.currentPlayer.transform.DOScale(scaleFactor, scaleDuration).SetEase(ease);

            animatorManager = soPlayer.currentPlayer.GetComponent<AnimatorManager>();
            animatorManager.Play(AnimatorManager.AnimationType.IDLE);
        }

        playerStatus.text = "";
        _currentSpeed = initialSpeed;
        ItemManager.Instance.UpdateTotalCoins();
    }


    void Update()
    {
        if (!canRun) return;
        _pos = target.position;
        _pos.y = target.position.y;
        _pos.z = transform.position.z;

        if (_win)
        {
            transform.Translate(_currentSpeed / 10 * Time.deltaTime * -transform.forward);
        }
        else
        {
            transform.Translate(_currentSpeed * Time.deltaTime * transform.forward);
        }
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
            transform.GetComponent<Collider>().enabled = false;
            _lose = true;
            GameManager.Instance.ChangeCamera();
            endScreen.SetActive(true);
            endScreen.GetComponent<Animator>().SetTrigger("lose");
            Invoke(nameof(ToInvokeEndGameLose), 0f);
        }

        Debug.Log(collision.gameObject.tag);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Endline"))
        {

            transform.GetComponent<Collider>().enabled = false;
            ItemManager.Instance.SaveCoinsData();
            _win = true;
            GameManager.Instance.ChangeCamera();
            soPlayer.currentPlayer.transform.eulerAngles = new(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z);
            endScreen.SetActive(true);
            endScreen.GetComponent<Animator>().SetTrigger("win");
            Invoke(nameof(ToInvokeEndGameWin), 3f);
        }
    }

    private void ToInvokeEndGameLose()
    {
        EndGame(AnimatorManager.AnimationType.DEAD);
    }

    private void ToInvokeEndGameWin()
    {
        EndGame(AnimatorManager.AnimationType.WIN);
    }


    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.WIN)
    {
        canRun = false;
        animatorManager.Play(animationType);
        ItemManager.Instance.UpdateTotalCoins();
    }

    public void StartToRun()
    {
        canRun = true;
        animatorManager = soPlayer.currentPlayer.GetComponent<AnimatorManager>();
        animatorManager.Play(AnimatorManager.AnimationType.RUN, (_currentSpeed / _baseSpeedToAnimation));
        transform.GetComponent<Collider>().enabled = true;
    }

    public void Attack()
    {
        target.GetComponent<TouchController>().currentPlayerCorroutine = StartCoroutine(OnAttack());
    }

    IEnumerator OnAttack()
    {
        animatorManager.Play(AnimatorManager.AnimationType.ATTACK);
        yield return new WaitForSeconds(0.3f);
        target.GetComponent<TouchController>().currentPlayerCorroutine = null;
        WhatAnimation();
    }


    public bool IsGrounded()
    {
        Collider pCollider = GetComponent<Collider>();
        float playerMiddleToBottomHeight = Mathf.Abs(pCollider.bounds.min.y - transform.position.y);

        debugOrigin = transform.position;
        debugDist = distToGround;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, playerMiddleToBottomHeight + distToGround))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                return true;
            }
        }

        return false;
    }


    private Vector3? debugOrigin = null;
    private float? debugDist;

    private void OnDrawGizmos()
    {
        if (debugOrigin == null) return;

        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(debugOrigin.Value, debugOrigin.Value + Vector3.down * (debugDist.Value));

    }



    public void WhatAnimation()
    {
        if (playerStatus.text == "Flying")
        {
            animatorManager.Play(AnimatorManager.AnimationType.FLYING);
        }
        else if (playerStatus.text == "SpeedUp")
        {
            animatorManager.Play(AnimatorManager.AnimationType.SPRINT);
        }
        else if (_win)
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
            animatorManager.Play(AnimatorManager.AnimationType.RUN, (_currentSpeed / _baseSpeedToAnimation));
        }
    }

    public void PowerUpInvincible(string statusName)
    {
        playerStatus.text = statusName;
        _invincible = true;
        playerCanvas.transform.DOLocalMoveY(2.1f, .5f).SetEase(Ease.Linear);
        animatorManager.transform.DOScale(1.5f, 0.5f).SetEase(Ease.Linear);
    }

    public void PowerUpInvincibleEnd(string statusName)
    {
        ResetStatusName(statusName);
        _invincible = false;
        playerCanvas.transform.DOLocalMoveY(1.3f, .5f).SetEase(Ease.Linear);
        animatorManager.transform.DOScale(1, 0.5f).SetEase(Ease.Linear);
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
        animatorManager.Play(AnimatorManager.AnimationType.RUN, (_currentSpeed / _baseSpeedToAnimation));
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




