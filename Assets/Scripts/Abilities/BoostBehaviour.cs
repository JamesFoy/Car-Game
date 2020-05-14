using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBehaviour : MonoBehaviour
{
    [HideInInspector] public float boostAmount;
    CarInfo carInfo;
    CarController carController;

    // Start is called before the first frame update
    void OnEnable()
    {
        carInfo = GetComponentInParent<CarInfo>();
        carController = GetComponentInParent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        carInfo.carStats.maxSpeed = boostAmount;

        if (carInfo.carStats.speed > 110)
        {
            carController.SpeedDots(true);
        }
    }

    private void OnDestroy()
    {
        carInfo.carStats.maxSpeed = 110;
        carController.SpeedDots(false);
    }
}
