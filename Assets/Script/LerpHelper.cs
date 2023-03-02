using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpHelper : MonoBehaviour
{
    public Transform target;
    public float lerpSpeed = 1f;

    void Start()
    {

    }


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, lerpSpeed * Time.deltaTime);
    }
}
