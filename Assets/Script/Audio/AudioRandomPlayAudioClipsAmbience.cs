using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioRandomPlayAudioClipsAmbience : MonoBehaviour
{
    public List<AudioClip> audioClipList;
    public AudioSource audioSource;


    private void Start()
    {
        PlayRandom();
    }


    public void PlayRandom()
    {     
        audioSource.clip = audioClipList[Random.Range(0, audioClipList.Count)];
        audioSource.Play();  
    }
}
