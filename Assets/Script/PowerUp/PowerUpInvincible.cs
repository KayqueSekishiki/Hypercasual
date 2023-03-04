using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvincible : PowerUpBase
{
    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.PowerUpInvincible(statusName);
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.PowerUpInvincibleEnd();
    }
}
