using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffectBehaviour : MonoBehaviour
{
    public GameObject damageEffectShader;

    CarInfo carInfo;

    float healthLastFrame;

    // Start is called before the first frame update
    void Start()
    {
        carInfo = GetComponent<CarInfo>();   
    }

    private void FixedUpdate()
    {
        healthLastFrame = carInfo.carStats.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (carInfo.carStats.health != healthLastFrame)
        {
            StartCoroutine(DamageFlash());
        }
    }

    IEnumerator DamageFlash()
    {
        damageEffectShader.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        damageEffectShader.SetActive(false);
    }
}
