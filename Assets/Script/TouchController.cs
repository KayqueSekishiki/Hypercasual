using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Vector2 _fingerDown;
    private Vector2 _fingerUp;

    public float swipeSensitivity = 20f;
    public float swipeVelocity = 1f;

    [Header("Jump Setup")]
    public bool jump = false;
    public float jumpDuration = 1f;
    public Coroutine currentPlayerCorroutine;


    public PlayerController playerController;


    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _fingerUp = touch.position;
                _fingerDown = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _fingerDown = touch.position;
                CheckSwipe();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            _fingerUp = Input.mousePosition;

        }

        if (Input.GetMouseButtonUp(0))
        {
            _fingerDown = Input.mousePosition;
            CheckSwipe();
        }
    }


    void CheckSwipe()
    {
        if (!playerController.canRun) return;

        if (VerticalMove() > swipeSensitivity && VerticalMove() > HorizontalValMove())
        {
            if (_fingerDown.y - _fingerUp.y > 0)
            {
                OnSwipeUp();
            }
            else if (_fingerDown.y - _fingerUp.y < 0)
            {
                OnSwipeDown();
            }
            _fingerUp = _fingerDown;
        }

        else if (HorizontalValMove() > swipeSensitivity && HorizontalValMove() > VerticalMove())
        {
            if (_fingerDown.x - _fingerUp.x > 0)
            {
                OnSwipeRight();
            }
            else if (_fingerDown.x - _fingerUp.x < 0)
            {
                OnSwipeLeft();
            }
            _fingerUp = _fingerDown;
        }
    }


    float VerticalMove()
    {
        return Mathf.Abs(_fingerDown.y - _fingerUp.y);
    }

    float HorizontalValMove()
    {
        return Mathf.Abs(_fingerDown.x - _fingerUp.x);
    }

    //RETORNOS
    void OnSwipeUp()
    {
        if (currentPlayerCorroutine != null) return;
        currentPlayerCorroutine = StartCoroutine(Jump());
        Debug.Log("Cima!");
    }

    void OnSwipeDown()
    {
        if (currentPlayerCorroutine != null) return;
        playerController.Attack();
        Debug.Log("Baixo!");
    }

    void OnSwipeLeft()
    {
        if (transform.position.x == -4) return;
        transform.position += Vector3.left * swipeVelocity;
        Debug.Log("Esquerda!");
    }

    void OnSwipeRight()
    {
        if (transform.position.x == 4) return;

        transform.position += Vector3.right * swipeVelocity;
        Debug.Log("Direita!");
    }

    IEnumerator Jump()
    {
        if (!jump)
        {
            jump = true;
            playerController.animatorManager.Play(AnimatorManager.AnimationType.JUMP);
        }

        yield return new WaitForSeconds(jumpDuration);

        jump = false;
        currentPlayerCorroutine = null;
        // playerController.GetComponentInChildren<AnimatorManager>().transform.localPosition = new Vector3(0, -0.5f, 0);
        playerController.WhatAnimation();
    }
}



