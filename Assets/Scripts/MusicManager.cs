using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    private float musicVolume = .3f;
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    public void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        musicVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, .3f);
    }

    public void ChangeMusicVolume()
    {
        musicVolume += .1f;
        if (musicVolume > 1f)
        {
            musicVolume = 0f;
        }

        audioSource.volume = musicVolume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, musicVolume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }
}
