using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{

    public event EventHandler<OnProgressChangedArgs> OnProgressChanged;
    public event EventHandler OnCut;

    public class OnProgressChangedArgs : EventArgs
    {
        public float progressNormalized;
    }
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchen object here
            if (player.HasKitchenObject()) {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    
                    OnProgressChanged?.Invoke(this, new OnProgressChangedArgs
                    {
                        progressNormalized = (float)cuttingProgress /  cuttingRecipeSO.cuttingProgressMax
                    });
                }
            }
            else
            {
                //player not carrying anything
            }
        } 
        else
        {
            //there is a kitchen object here
            if (!player.HasKitchenObject())
            {
                //player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                //player carrying somehting
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            
            OnProgressChanged?.Invoke(this, new OnProgressChangedArgs
            {
                progressNormalized = (float)cuttingProgress /  cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroyKitchenObject();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
            else
            {
                
            }
        }
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO currentKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(currentKitchenObjectSO);

        return cuttingRecipeSO != null ? cuttingRecipeSO.output : null;
    }
    private bool HasRecipeWithInput(KitchenObjectSO currentKitchenObjectSO)
    {
        return GetCuttingRecipeSOWithInput(currentKitchenObjectSO) != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO kitchenObjectSo)
    {
        foreach (var cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == kitchenObjectSo)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
