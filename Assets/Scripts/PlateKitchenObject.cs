using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO>  validKitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            Debug.Log("TryAddIngredient: Invalid Ingredient");
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //already has this type
            Debug.Log("TryAddIngredient: Plate already has attempted ingredient.");
            return false;
        }
        kitchenObjectSOList.Add(kitchenObjectSO);
        return true;
    }
}
