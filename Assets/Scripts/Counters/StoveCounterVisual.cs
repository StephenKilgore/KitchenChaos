using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject particlesGameObject;
    // Start is called before the first frame update
    void Start()
    {
        stoveCounter.OnStateChange += StoveCounterOnOnStateChange;
    }

    private void StoveCounterOnOnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stoveOnGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
