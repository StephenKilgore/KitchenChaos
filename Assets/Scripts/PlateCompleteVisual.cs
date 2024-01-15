using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
   [Serializable]
   public struct KitchenObjectSO_GameObject
   {
      public KitchenObjectSO kitchenObjectSO;
      public GameObject gameObject;
   }
   [SerializeField] private PlateKitchenObject plateKichenObject;
   [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

   private void Start()
   {
      plateKichenObject.OnIngredientAdded += PlateKichenObjectOnOnIngredientAdded;
      foreach (KitchenObjectSO_GameObject kitchenObjectSoGameObject in kitchenObjectSOGameObjectList)
      {
         kitchenObjectSoGameObject.gameObject.SetActive(false);
      }
   }

   private void PlateKichenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedArgs e)
   {
      foreach (KitchenObjectSO_GameObject kitchenObjectSoGameObject in kitchenObjectSOGameObjectList)
      {
         if (kitchenObjectSoGameObject.kitchenObjectSO == e.ingredientAdded)
         {
            kitchenObjectSoGameObject.gameObject.SetActive(true);
         }
      }
   }
}
