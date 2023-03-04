using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    //public Collider collider;
    public int coinValue;

    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddCoins(coinValue);
    }

    public void GetCoins()
    {
        OnCollect();
    }
}
