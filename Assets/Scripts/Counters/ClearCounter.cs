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
        if (!HasKitchenObject())
        {
            //there is no kitchen object here
            if (player.HasKitchenObject())
            {
                //player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player not carrying anything
            }
        }
        else
        {
            // there is a kitchen object here
            if (player.HasKitchenObject())
            {
                //player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // player is  holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        Destroy(GetKitchenObject().gameObject);
                    }
                }
                else
                {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //counter is holding a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()));
                        {
                            Destroy(GetKitchenObject().gameObject);
                        }
                    }    
                }
            }
            else
            {
                // player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
