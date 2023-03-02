using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;

    void Start()
    {

    }


    void Update()
    {
        transform.Translate(speed * Time.deltaTime * transform.forward);
    }
}
