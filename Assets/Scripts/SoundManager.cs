using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SoundManager : MonoBehaviour
{
    
    [SerializeField] private AudioClipRefSO audioClipRefSO;
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume=1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume=1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
        
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManagerOnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManagerOnRecipeFailed;
    }

    private void DeliveryManagerOnRecipeFailed(object sender, EventArgs e)
    {
        PlaySound(audioClipRefSO.deliveryFail, Camera.main.transform.position);
    }

    private void DeliveryManagerOnRecipeSuccess(object sender, EventArgs e)
    {
        PlaySound(audioClipRefSO.deliverySuccess, Camera.main.transform.position);
    }
}
