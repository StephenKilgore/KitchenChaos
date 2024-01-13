using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;
    
    private KitchenObject kitchenObject;
    
    public void Interact()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
               kitchenObject.SetClearCounter(secondClearCounter);
               Debug.Log(kitchenObject.GetClearCounter());
            }
        }
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(KitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;
            
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetClearCounter(this);
            Debug.Log(kitchenObject.GetClearCounter());
        }
    }
}
