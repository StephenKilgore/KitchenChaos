using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        GameManager.Instance.OnStateChange += GameManagerOnStateChange;
        Hide();
    }

    public void Update()
    {
        if (GameManager.Instance.GetState() == GameManager.State.CountdownToStart)
        {
            int seconds = (int) GameManager.Instance.GetCountdownToStartTimer();
            countdownText.text =  seconds.ToString();
        }
    }

    private void GameManagerOnStateChange(object sender, EventArgs e)
    {
        if (GameManager.Instance.GetState() == GameManager.State.CountdownToStart)
        {
            Show();
        }
        else
        {
            Hide();
        }
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
