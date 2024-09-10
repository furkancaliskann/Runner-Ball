using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    AudioSource source;
    Settings settings;

    public AudioClip bonusAudioClip;
    public AudioClip gameOverAudioClip;
    public AudioClip clickAudioClip;

    void Start()
    {
        source = GetComponent<AudioSource>();
        settings = GetComponent<Settings>();
    }

    
    public void PlayBonusClip()
    {
        if (!settings.isSoundOn) return;
        source.PlayOneShot(bonusAudioClip);
    }

    public void PlayGameOverClip()
    {
        if (!settings.isSoundOn) return;
        source.PlayOneShot(gameOverAudioClip);
    }

    public void PlayClickSound()
    {
        if (!settings.isSoundOn) return;
        source.PlayOneShot(clickAudioClip);
    }
}
