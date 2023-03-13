using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemCollectableBase : MonoBehaviour
{
    public string compateTag = "Player";
    public ParticleSystem myParticleSystem;

    [Header("Sounds")]
    public AudioSource audioSource;




    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compateTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        HideItems();
        OnCollect();
    }

    protected virtual void HideItems()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        if (myParticleSystem != null)
        {
            myParticleSystem.transform.SetParent(null);
            myParticleSystem.Play();
        }

        if (audioSource != null)
        {
            audioSource.transform.SetParent(null);
            audioSource.Play();
        }
    }

}
