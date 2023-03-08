using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Ebac.Core.Singleton;

public class Teste : Singleton<CoinsAnimationManager>
{
    [Header("Animations")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;

    public List<ItemCollectableCoin> items;

    private void Start()
    {
        items = new List<ItemCollectableCoin>();
    }

    public void RegisterCoin(ItemCollectableCoin i)
    {
        if (!items.Contains(i))
        {
            items.Add(i);
            i.transform.localScale = Vector3.zero;
        }
    }

    public void UnRegisterCoin(ItemCollectableCoin i)
    {
        //if (!items.Contains(i))
        //{
        //    items.Remove(i);
        //}
        items.Clear();
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) StartAnimations();
    }

    public void StartAnimations()
    {
        StartCoroutine(ScalePiecesByType());
    }

    IEnumerator ScalePiecesByType()
    {
        foreach (var coin in items)
        {
            coin.transform.localScale = Vector3.zero;
        }

        Sort();
        yield return null;

        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenPieces);
        }
    }


    private void Sort()
    {
        items = items.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }
}
