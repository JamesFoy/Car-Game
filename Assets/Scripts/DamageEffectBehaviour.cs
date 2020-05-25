using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffectBehaviour : MonoBehaviour
{
    public GameObject damageEffectShader;
    public GameObject damageEffects10;
    public GameObject damageEffects30;
    public GameObject damageEffects60;
    public GameObject deathEffect;

    CarStockCounter stockCounter;

    bool deathCheck = false;

    CarInfo carInfo;

    float healthLastFrame;

    // Start is called before the first frame update
    void Start()
    {
        stockCounter = GetComponent<CarStockCounter>();
        carInfo = GetComponent<CarInfo>();   
    }

    //Saves the health value on the previous frame to a variable
    private void FixedUpdate()
    {
        healthLastFrame = carInfo.carStats.health;
    }

    // Update is called once per frame
    void Update()
    {
        //checks if the health value isnt equal to the previous frames value (if it isnt the same the player has taken damage!!!)
        if (carInfo.carStats.health != healthLastFrame)
        {
            StartCoroutine(DamageFlash());
        }

        if (carInfo.carStats.health >= 10)
        {
            damageEffects10.SetActive(true);
        }
        else
        {
            damageEffects10.SetActive(false);
        }

        if (carInfo.carStats.health >= 30)
        {
            damageEffects30.SetActive(true);
        }
        else
        {
            damageEffects30.SetActive(false);
        }

        if (carInfo.carStats.health >= 60)
        {
            damageEffects60.SetActive(true);
        }
        else
        {
            damageEffects60.SetActive(false);
        }

        if (carInfo.carStats.health >= 100)
        {
            if (deathCheck == false)
            {
                deathCheck = true;
                CheckpointManager.Instance.ResetThisGameObjectToItsLastCheckpoint(gameObject);
                stockCounter.ReduceThisCarStockByOne();
                GameObject deathEffectClone = Instantiate(deathEffect, transform.position, transform.rotation);
            }
        }
    }

    //Enumerator used to make the damage flash last half a second
    IEnumerator DamageFlash()
    {
        damageEffectShader.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        damageEffectShader.SetActive(false);
    }
}
