using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicManager : MonoBehaviour
{

    public static AudioClip LAST_CLIP = null;
    public static float LAST_POSITION = 0f;

    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource.clip == LAST_CLIP)
        {
            audioSource.time = LAST_POSITION;
        }
    }

    void Update()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        LAST_CLIP = audioSource.clip;
        LAST_POSITION = GetComponent<AudioSource>().time;
    }
}