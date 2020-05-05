using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarUIManager : MonoBehaviour
{
    public TMP_Text speedText; //Reference to the HUD text field for the speed
    public TMP_Text damageText; //Reference to the HUD text field for the damage

    CarInfo carInfo;

    private void Start()
    {
        carInfo = GetComponent<CarInfo>();
    }
    private void Update()
    {
        float clampedSpeed = Mathf.Clamp(carInfo.carStats.speed, 0, carInfo.carStats.maxSpeed);

        speedText.text = System.Math.Round(clampedSpeed, 0).ToString();
        damageText.text = System.Math.Round(carInfo.carStats.health).ToString();
    }
}
