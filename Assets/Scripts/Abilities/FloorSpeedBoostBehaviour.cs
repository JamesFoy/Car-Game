using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpeedBoostBehaviour : MonoBehaviour
{
    public GameObject particleEffect;
    float powerupAmount = 200;
    PowerupTriggerable powerupTriggerable;

    private void OnTriggerEnter(Collider other)
    {
        //If the ability name is shield, spawn it and set the boost amount to the powerupAmount
        if (other.gameObject.CompareTag("Player"))
        {
            powerupTriggerable = other.GetComponentInChildren<PowerupTriggerable>();
            Debug.Log("Player a boost zone");

            //Instantiate a copy of the powerup and store it in a new rigidbody variable called clondedPowerup
            GameObject clondedPowerup = Instantiate(particleEffect, powerupTriggerable.exaustParticleSpawnPoint.transform.position, powerupTriggerable.exaustParticleSpawnPoint.rotation, powerupTriggerable.exaustParticleSpawnPoint.transform) as GameObject;

            clondedPowerup.GetComponent<BoostBehaviour>().boostAmount = powerupAmount; //Set the boostAmount to the powerAmount
        }
    }
}
