using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEffectBehaviour : MonoBehaviour
{
    CarInfo carInfo;

    // Start is called before the first frame update
    void OnEnable()
    {
        carInfo = GetComponentInParent<CarInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        carInfo.carStats.maxSpeed = 70;
    }

    private void OnDestroy()
    {
        carInfo.carStats.maxSpeed = 100;
    }
}
