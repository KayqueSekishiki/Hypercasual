using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : ItemCollectableBase
{
    [Header("PowerUp")]
    public string statusName;
    public float duration;

    protected override void OnCollect()
    {
        base.OnCollect();
        StartPowerUp();
        PlayerController.Instance.bounceHelper.Bounce();
    }

    protected virtual void StartPowerUp()
    {
        Debug.Log("Start PowerUp: " + statusName);
        Invoke(nameof(EndPowerUp), duration);
    }

    protected virtual void EndPowerUp()
    {
        Debug.Log("End PowerUp: " + statusName);
    }
}
