using System;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public static AudioSource AudioSource { private set; get; }

    private void Awake()
    {
        AudioSource = this.GetComponent<AudioSource>();
    }

    public static void PlaySe(AudioClip clip)
    {
        AudioSource.PlayOneShot(clip);
    }
    
}
