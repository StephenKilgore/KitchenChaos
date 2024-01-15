using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private CuttingCounter cuttingCounter;

    private void Start()
    {
        cuttingCounter.OnProgressChanged += CuttingCounterOnOnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void CuttingCounterOnOnProgressChanged(object sender, CuttingCounter.OnProgressChangedArgs e)
    {
        if (e.progressNormalized > 0 && !gameObject.activeSelf)
        {
            Show();
        }
        barImage.fillAmount = e.progressNormalized;

        if (gameObject.activeSelf && e.progressNormalized >= 1f)
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
