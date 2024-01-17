using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += InstanceOnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeDelivered += InstanceOnOnRecipeDelivered;
        
        UpdateVisual();
    }

    private void InstanceOnOnRecipeDelivered(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void InstanceOnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach ( RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipes())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
        
    }
}
