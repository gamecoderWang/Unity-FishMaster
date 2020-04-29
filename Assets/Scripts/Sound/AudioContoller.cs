using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioContoller : MonoBehaviour {

    private static AudioContoller instance;

    public static AudioContoller Instance
    {
        get
        {
            if (instance == null)
                return GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioContoller>();
            return instance;
        }
    }

    public AudioSource bgmAudioSourse;
    public AudioSource otherAudioSourse;

    public AudioClip startSceneAudio;
    public AudioClip upLevelAudio;                                 
    public AudioClip getSmallRewardAudio;
    public AudioClip getBigRewardAudio;
    public AudioClip goldSharkDieAudio;
    public AudioClip changeBackgroundAudio;
    public AudioClip fishDieAudio;
    public AudioClip bulletAudio;
    public AudioClip changeWeaponAudio;
    public AudioClip clickButtonAudio;
    public AudioClip[] bgmAudios;

    public void ChangeBgmAudio(AudioClip clip)
    {
        bgmAudioSourse.clip = clip;
        bgmAudioSourse.Play();
    }

    public void PlayOtherAudio(AudioClip clip)
    {
        otherAudioSourse.PlayOneShot(clip);
    }

}
