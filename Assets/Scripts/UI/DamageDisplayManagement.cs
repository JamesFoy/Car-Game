using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageDisplayManagement : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public CarInfo carInfo;

    private void Update()
    {
        damageText.text = carInfo.carStats.health.ToString("n0") + "%";
    }
}