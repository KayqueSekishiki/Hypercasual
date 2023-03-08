using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BounceHelper : MonoBehaviour
{

    [Header("Animations")]
    public float scaleDuration = .05f;
    public float scaleBounce = 1.1f;
    public Ease ease = Ease.Linear;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Bounce();
        }
    }


    public void Bounce()
    {
        transform.DOScale(scaleBounce, scaleBounce).SetEase(ease).SetLoops(2, LoopType.Yoyo);
    }
}
