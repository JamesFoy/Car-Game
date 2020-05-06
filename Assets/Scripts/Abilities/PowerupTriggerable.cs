using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject particleEffect;
    public Transform centreParticleSpawnPoint;
    public Transform exaustParticleSpawnPoint;
    [HideInInspector] public float powerupAmount = 0;

    public void Activate(Ability thisAbility)
    {
        //Add powerupAmount to the player (can be shield length, heal amount, speed boost amount)

        if (thisAbility.Name == "Repair")
        {
            Debug.Log("This is the Repair ability");

            //Instantiate a copy of the projectile and store it in a new rigidbody variable called clonedProjectile
            GameObject clonedProjectile = Instantiate(particleEffect, centreParticleSpawnPoint.transform.position, centreParticleSpawnPoint.rotation, centreParticleSpawnPoint.transform) as GameObject;

            clonedProjectile.transform.Rotate(-90, 90, 0); //Setting the correct rotation for the repair effect
            clonedProjectile.GetComponent<RepairBehaviour>().RepairDamage(powerupAmount);
        }

        if (thisAbility.Name == "Shield")
        {
            Debug.Log("This is the Shield ability");

            //Instantiate a copy of the projectile and store it in a new rigidbody variable called clonedProjectile
            GameObject clonedProjectile = Instantiate(particleEffect, centreParticleSpawnPoint.transform.position, centreParticleSpawnPoint.rotation, centreParticleSpawnPoint.transform) as GameObject;
        }

        if (thisAbility.Name == "Teleport")
        {
            Debug.Log("This is the Teleport ability");

            //Instantiate a copy of the projectile and store it in a new rigidbody variable called clonedProjectile
            GameObject clonedProjectile = Instantiate(particleEffect, centreParticleSpawnPoint.transform.position, centreParticleSpawnPoint.rotation, centreParticleSpawnPoint.transform) as GameObject;
        }

        if (thisAbility.Name == "Boost")
        {
            Debug.Log("This is the Boost ability");

            //Instantiate a copy of the projectile and store it in a new rigidbody variable called clonedProjectile
            GameObject clonedProjectile = Instantiate(particleEffect, exaustParticleSpawnPoint.transform.position, exaustParticleSpawnPoint.rotation, exaustParticleSpawnPoint.transform) as GameObject;
        }

        if (thisAbility.Name == "Flamethrower")
        {
            Debug.Log("This is the Flamethrower ability");

            //Instantiate a copy of the projectile and store it in a new rigidbody variable called clonedProjectile
            GameObject clonedProjectile = Instantiate(particleEffect, exaustParticleSpawnPoint.transform.position, exaustParticleSpawnPoint.rotation, exaustParticleSpawnPoint.transform) as GameObject;
            clonedProjectile.transform.Rotate(0, 90, 90); //Setting the correct rotation for the repair effect
        }
    }
}
