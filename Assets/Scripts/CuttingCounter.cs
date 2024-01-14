using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject() && player.GetKitchenObject().GetKitchenObjectSO().isCuttable)
            {
                Debug.Log(player.GetKitchenObject().GetKitchenObjectSO().isCuttable);
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            KitchenObject currentKitchenObject = GetKitchenObject();
            KitchenObjectSO currentKitchenObjectSO = currentKitchenObject.GetCutKitchenObjectSO();
            GetKitchenObject().DestroyKitchenObject();
            KitchenObject.SpawnKitchenObject(currentKitchenObjectSO, this);
        }
    }
}
