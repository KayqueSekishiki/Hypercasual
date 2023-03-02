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


    private void Awake()
    {
        if (myParticleSystem != null)
        {
            myParticleSystem.transform.SetParent(parentVFX);
        }
        if (audioSource != null)
        {
            audioSource.transform.SetParent(parentSFX);
        }


    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compateTag))
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        gameObject.SetActive(false);
        OnCollect();
    }

    protected virtual void OnCollect()
    {
        if (myParticleSystem != null) myParticleSystem.Play();
      //  if (audioSource != null) audioRandomPlayAudioClips.PlayRandom();
    }
}
