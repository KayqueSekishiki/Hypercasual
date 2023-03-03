using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class PlayerController : Singleton<PlayerController>
{
    //publics
    [Header("Lerp Config")]
    public Transform target;
    public float lerpSpeed = 1f;


    [Header("Player")]
    public float initialSpeed = 1f;

    public GameObject endScreen;

    //privates
    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;


    private void Start()
    {
        _currentSpeed = initialSpeed;
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
        if (collision.transform.CompareTag("Enemy"))
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

    public void PowerUpSpeedUp(float addSpeed)
    {
        _currentSpeed += addSpeed;
    }

    public void PowerUpSpeedUpEnd()
    {
        _currentSpeed = initialSpeed;
    }




    #endregion
}




