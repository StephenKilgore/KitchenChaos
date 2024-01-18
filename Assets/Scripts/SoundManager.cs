using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioClipRefSO audioClipRefSO;

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    private float sfxVolume = 1f;
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier=1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, sfxVolume);
    }
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier=1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, sfxVolume*volumeMultiplier);
        
        
    }

    private void Awake()
    {
        Instance = this;
        sfxVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManagerOnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManagerOnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounterOnAnyCut;
        Player.Instance.OnPickedSomething += InstanceOnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounterOnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounterOnAnyObjectTrashed; 
    }

    private void TrashCounterOnAnyObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefSO.trash, trashCounter.transform.position);
    }

    private void BaseCounterOnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefSO.objectDrop, baseCounter.transform.position);
    }

    private void InstanceOnPickedSomething(object sender, EventArgs e)
    {
        PlaySound(audioClipRefSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounterOnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManagerOnRecipeFailed(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManagerOnRecipeSuccess(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefSO.deliverySuccess, deliveryCounter.transform.position);
    }

    public void PlayFootstepsSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipRefSO.footsteps, position, volumeMultiplier*sfxVolume);
    }

    public void ChangeSFXVolume()
    {
        sfxVolume += .1f;
        if (sfxVolume > 1f)
        {
            sfxVolume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, sfxVolume);
        PlayerPrefs.Save();
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }
}
