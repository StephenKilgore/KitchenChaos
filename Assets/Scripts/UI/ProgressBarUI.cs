using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject hasProgressGameObject;
    
    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        hasProgress.OnProgressChanged += HasPr_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasPr_OnProgressChanged(object sender, IHasProgress.OnProgressChangedArgs e)
    {
        if (e.progressNormalized > 0 && !gameObject.activeSelf)
        {
            Show();
        }
        barImage.fillAmount = e.progressNormalized;

        if (gameObject.activeSelf && e.progressNormalized >= 1f || e.progressNormalized == 0)
        {
            Hide();
            barImage.fillAmount = 0;
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
