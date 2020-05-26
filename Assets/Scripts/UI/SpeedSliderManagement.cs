using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedSliderManagement : MonoBehaviour
{
    public List<GameObject> sliderBars;
    public CarInfo carInfo;
    public TextMeshProUGUI speedText;

    private void Update()
    {
        if (carInfo != null)
        {
            for (int i = 0; i < sliderBars.Count; i++)
            {
                sliderBars[i].SetActive(false);
            }

            float barValue = 200 / sliderBars.Count;
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
            if (speedText != null)
            {
                speedText.text = carInfo.carStats.speed.ToString("n0");
            }
        }
        else
        {
            Debug.Log("No car info assigned to speed slider");
        }
    }
}
