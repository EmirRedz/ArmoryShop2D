using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayMusic(AudioSource audioSource , float seconds, bool isLoop)
    {
        audioSource.volume = 0;
        SetVolume(audioSource, isLoop, seconds, true);
    }
    public void StopMusic(AudioSource audioSource, float seconds)
    {
        SetVolume(audioSource, true, seconds, false);
    }

    public void SwapTracks(AudioSource audioSourceA, AudioSource audioSourceB,float lowerTime,float riseTime,bool isLoop)
    {
        StopMusic(audioSourceA, lowerTime);
        PlayMusic(audioSourceB, riseTime, isLoop);
    }


    private void SetVolume(AudioSource audioSource, bool isLoop, float seconds, bool isPlaying)
    {
        audioSource.loop = isLoop;
        audioSource.Play();
        var volume = isPlaying ? PlayerPrefs.GetFloat("MusicVolume") * PlayerPrefs.GetFloat("MasterVolume") : 0;

        audioSource.DOFade(volume, seconds).SetEase(Ease.Linear);

    }
}
