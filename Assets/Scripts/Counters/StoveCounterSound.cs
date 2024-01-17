using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private StoveCounter stoveCounter;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounterOnStateChange;
    }

    private void StoveCounterOnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        if (e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Frying)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
