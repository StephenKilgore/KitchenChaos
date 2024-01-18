using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    
    private void Awake()
    {
        Instance = this;
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeMusicVolume();
            UpdateVisual();
        });
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeSFXVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManagerOnGameUnpaused;
        UpdateVisual();
        Hide();
    }

    private void GameManagerOnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectText.text = $"SOUND EFFECTS: {Math.Round(SoundManager.Instance.GetSFXVolume() * 10)}";
        musicText.text = $"Music: {Math.Round(MusicManager.Instance.GetMusicVolume() * 10)}";
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
