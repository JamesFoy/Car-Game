using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffectBehaviour : MonoBehaviour
{
    public GameObject damageEffectShader;

    CarInfo carInfo;

    // Start is called before the first frame update
    void Start()
    {
        carInfo = GetComponent<CarInfo>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (carInfo.carStats.health >= 4)
        {
            damageEffectShader.SetActive(true);
        }
        else
        {
            damageEffectShader.SetActive(false);
        }
    }
}
