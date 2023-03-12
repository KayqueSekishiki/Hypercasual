using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compateTag = "Player";
    public ParticleSystem myParticleSystem;
    public Transform parentVFX;
    public Transform parentSFX;

    [Header("Sounds")]
    public AudioSource audioSource;
    //public AudioRandomPlayAudioClips audioRandomPlayAudioClips;


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

        //if (audioSource != null)
        //{
        //    audioRandomPlayAudioClips.transform.SetParent(null);
        //    audioRandomPlayAudioClips.PlayRandom();
        //}
    }
}
