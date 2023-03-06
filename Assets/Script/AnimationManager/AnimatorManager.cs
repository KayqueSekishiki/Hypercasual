using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    public List<AnimatorSetup> animatorSetups;

    public enum AnimationType
    {
        IDLE,
        RUN,
        SPRINT,
        DEAD,
        STAYDEAD,
        WIN,
        FLYING,
        ATTACK
    }

    public void Play(AnimationType type, float currentSpeedFactor = 1f)
    {

        animatorSetups.ForEach(animation =>
        {
            if (animation.type == type)
            {
                animator.SetTrigger(animation.trigger);
                animator.speed = animation.speed * currentSpeedFactor;
            }
        });

        //foreach (var animation in animatorSetups)
        //{
        //    if (animation.type == type)
        //    {
        //        animator.SetTrigger(animation.trigger);
        //        break;
        //    }
        //}
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Play(AnimationType.IDLE);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            Play(AnimationType.RUN);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            Play(AnimationType.SPRINT);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            Play(AnimationType.DEAD);
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            Play(AnimationType.STAYDEAD);
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            Play(AnimationType.WIN);
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            Play(AnimationType.FLYING);
        }
        else if (Input.GetKey(KeyCode.Alpha8))
        {
            Play(AnimationType.ATTACK);
        }
    }
}

[System.Serializable]
public class AnimatorSetup
{
    public AnimatorManager.AnimationType type;
    public string trigger;
    public float speed = 1f;
}
