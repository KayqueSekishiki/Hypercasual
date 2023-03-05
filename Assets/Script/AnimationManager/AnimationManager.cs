using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;
    public List<AnimatorSetup> animatorSetups;

    public enum AnimationType
    {
        IDLE,
        RUN,
        SPRINT,
        DEAD
    }

    public void Play(AnimationType type)
    {

        animatorSetups.ForEach(i => { if (i.type == type) { animator.SetTrigger(i.trigger); } });

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
    }
}

[System.Serializable]
public class AnimatorSetup
{
    public AnimationManager.AnimationType type;
    public string trigger;
}
