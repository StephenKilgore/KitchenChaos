using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public bool isCuttable;
    public KitchenObjectSO cutKitchenObjectSO;
    public Sprite sprite;
    public string objectName;
}
