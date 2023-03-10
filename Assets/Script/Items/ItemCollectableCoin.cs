using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    [Header("Coin")]
    public Collider myCollider;
    public bool collect = false;
    public float lerpTime = 5f;
    public float minDistance = 1f;
    public int coinValue;

    private void Start()
    {
        CoinsAnimationManager.Instance.RegisterCoin(this);
    }

    protected override void OnCollect()
    {
        base.OnCollect();
        myCollider.enabled = false;
        collect = true;
        ItemManager.Instance.AddCoins(coinValue);
    }

    protected override void Collect()
    {
        OnCollect();
    }

    private void Update()
    {
        if (collect)
        {
            transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position, lerpTime * Time.deltaTime);
            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < minDistance)
            {
                HideItems();
                Invoke(nameof(AutoDestroy), 5f);
            }
        }
    }

    private void AutoDestroy()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        CoinsAnimationManager.Instance.UnRegisterCoin(this);
    }


}
