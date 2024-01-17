using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    public void Start()
    {
        GameManager.Instance.OnStateChange += GameManagerOnStateChange;
        Hide();
    }

    private void GameManagerOnStateChange(object sender, EventArgs e)
    {
        if (GameManager.Instance.GetState() == GameManager.State.GameOver)
        {
            recipesDeliveredText.text = GameManager.Instance.GetRecipesDelivered().ToString();
            Show();
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

