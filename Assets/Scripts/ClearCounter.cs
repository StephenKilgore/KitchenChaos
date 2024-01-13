using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    public void Interact()
    {
       Transform kitchenObjectTransform = Instantiate(KitchenObjectSO.prefab, counterTopPoint);
       kitchenObjectTransform.localPosition = Vector3.zero;

       Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().getKtchenObjectSO().objectName);

    }
}
