using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject particleEffect;
    [HideInInspector] public float powerupAmount = 0;

    public void Activate()
    {
        //Instantiate a copy of the projectile and store it in a new rigidbody variable called clonedProjectile
        GameObject clonedProjectile = Instantiate(particleEffect, this.transform.position, transform.rotation, this.transform) as GameObject;

        //Add powerupAmount to the player (can be shield length, heal amount, speed boost amount)
        
    }
}
