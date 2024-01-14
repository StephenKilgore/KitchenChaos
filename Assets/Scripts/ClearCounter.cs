using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;

    public override void Interact(Player player)
    {
        // if (!HasKitchenObject())
        // {
        //     if (player.HasKitchenObject())
        //     {
        //         player.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        //     }
        // }
        // else
        // {
        //     if (!player.HasKitchenObject())
        //     {
        //         GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        //     }
        // }
    }
}
