using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] List<AudioClip> audioList;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeMusic()
    {
        audioSource.Stop();
        audioSource.clip = audioList[1];
        audioSource.Play();
    }
}
