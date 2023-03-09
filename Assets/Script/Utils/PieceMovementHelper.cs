using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PieceMovementHelper : MonoBehaviour
{
    public enum MovementType
    {
        NoAnimation,
        Rotate360,
        LeftToRight
    }

    public MovementType currentAnimation;

    public float animationDuration = 1f;

    private void Update()
    {
        switch (currentAnimation)
        {
            case MovementType.NoAnimation:
                break;
            case MovementType.Rotate360:
                transform.DORotate(new(0, 180, 0), animationDuration).SetEase(Ease.Linear);
                break;
            case MovementType.LeftToRight:
                //transform.DOLocalRotate(new(0, 180, 0), animationDuration);
                break;

        }
    }
}
