using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject particleEffect;
    public Transform particleSpawnPoint;
    [HideInInspector] public float powerupAmount = 0;

    public void Activate(Ability thisAbility)
    { 
        //Add powerupAmount to the player (can be shield length, heal amount, speed boost amount)

        if (thisAbility.Name == "Repair")
        {
            Debug.Log("This is the Repair ability");

            //Instantiate a copy of the projectile and store it in a new rigidbody variable called clonedProjectile
            GameObject clonedProjectile = Instantiate(particleEffect, particleSpawnPoint.transform.position, particleSpawnPoint.rotation, particleSpawnPoint.transform) as GameObject;

            clonedProjectile.transform.Rotate(-90, 90, 0); //Setting the correct rotation for the repair effect
            clonedProjectile.GetComponent<RepairBehaviour>().RepairDamage(powerupAmount);
        }
        else if (thisAbility.Name == "Shield")
        {
            Debug.Log("This is the Shield ability");

            //Instantiate a copy of the projectile and store it in a new rigidbody variable called clonedProjectile
            GameObject clonedProjectile = Instantiate(particleEffect, particleSpawnPoint.transform.position, particleSpawnPoint.rotation, particleSpawnPoint.transform) as GameObject;
        }
    }
}
