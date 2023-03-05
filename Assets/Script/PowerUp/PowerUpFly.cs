using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFly : PowerUpBase
{
    [Header("PowerUp Fly")]
    public float amountToHeight;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.PowerUpFly(statusName, amountToHeight);
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.PowerUpFlyEnd(statusName, amountToHeight);
    }
}
