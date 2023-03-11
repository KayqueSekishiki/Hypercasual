using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMagnet : PowerUpBase
{
    [Header("PowerUp GetCoins")]
    public float sizeAmount = 20f;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.PowerUpGetCoins(statusName, sizeAmount);
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.PowerUpGetCoinsEnd(statusName);
    }
}
