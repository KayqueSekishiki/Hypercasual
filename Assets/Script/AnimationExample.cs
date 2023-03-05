using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationExample : MonoBehaviour
{
    public Animation myAnimation;

    public AnimationClip idle;
    public AnimationClip run;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayAnimation(run);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayAnimation(idle);
        }
    }

    private void PlayAnimation(AnimationClip c)
    {
        myAnimation.CrossFade(c.name);
    }
}
