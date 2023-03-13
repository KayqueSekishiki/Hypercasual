using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;


public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioSource audioSource;
    public List<AudioClip> audioClipList;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.clip = audioClipList[0];
        audioSource.Play();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        audioSource.clip = audioClipList[1];
        audioSource.Play();
    }
}
