using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGetCoins : PowerUpBase
{
    [Header("PowerUp GetCoins")]
    public float sizeAmount = 10f;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.PowerUpGetCoins(statusName, sizeAmount);
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.PowerUpGetCoinsEnd();

    }
}
