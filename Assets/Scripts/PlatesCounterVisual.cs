using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform countertopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlatesCounter plateCounter;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }
    private void Start()
    {
        plateCounter.OnPlateSpawned += PlateCounterOnOnPlateChanged;
        plateCounter.OnPlateRemoved += PlateCounterOnPlateRemoved;
    }

    private void PlateCounterOnOnPlateChanged(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, countertopPoint);
        
        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);
        
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }

    private void PlateCounterOnPlateRemoved(object sender, EventArgs e)
    {
        if (plateVisualGameObjectList.Count > 0)
        {
            GameObject lastPlate = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
            Destroy(lastPlate);
            plateVisualGameObjectList.Remove(lastPlate);
        }
    }
}
