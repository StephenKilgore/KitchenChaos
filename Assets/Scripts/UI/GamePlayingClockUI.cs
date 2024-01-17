using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image gameClockImage;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetState() == GameManager.State.GamePlaying)
        {
            gameClockImage.fillAmount = GameManager.Instance.GetGameTimeRemainingNormalized();
        }
    }
}
