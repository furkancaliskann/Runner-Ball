using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    AudioPlayer audioPlayer;

    public Image soundImage1, soundImage2;
    public Sprite soundOnSprite, soundOffSprite;
    public bool isSoundOn;

    void Start()
    {
        audioPlayer = GetComponent<AudioPlayer>();
        GetSound();
    }

    void GetSound()
    {
        isSoundOn = PlayerPrefs.GetInt("sound", 1) == 1 ? true : false;

        if(isSoundOn)
        {
            soundImage1.sprite = soundImage2.sprite = soundOnSprite;
            isSoundOn = true;
        }
        else
        {
            soundImage1.sprite = soundImage2.sprite = soundOffSprite;
            isSoundOn = false;
        }
    }

    public void SetSound()
    {
        isSoundOn = !isSoundOn;

        if (isSoundOn)
        {
            PlayerPrefs.SetInt("sound", 1);
            soundImage1.sprite = soundImage2.sprite = soundOnSprite;
        }
        else
        {
            PlayerPrefs.SetInt("sound", 0);
            soundImage1.sprite = soundImage2.sprite = soundOffSprite;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
