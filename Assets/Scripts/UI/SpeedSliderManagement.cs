using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSliderManagement : MonoBehaviour
{
    public List<GameObject> sliderBars;
    public CarInfo carInfo;

    private void Update()
    {
        for (int i = 0; i < sliderBars.Count; i++)
        {
            sliderBars[i].SetActive(false);
        }

        float barValue = carInfo.carStats.maxSpeed / sliderBars.Count;
        int numberOfBarsToEnable = Mathf.FloorToInt(carInfo.carStats.speed / barValue);

        if (numberOfBarsToEnable <= sliderBars.Count)
        {
            for (int i = 0; i < numberOfBarsToEnable; i++)
            {
                sliderBars[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < sliderBars.Count; i++)
            {
                sliderBars[i].SetActive(true);
            }
        }
    }
}
