using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for spawning a powerup once a power up is triggred elsewhere
public class PowerupTriggerable : MonoBehaviour
{
    [HideInInspector] public GameObject particleEffect; //Variable for setting the "Visual" for the powerup ability (this will be the prefab spawned that will contain the behaviour for the ability)

    //Variables containing transforms for spawn locations of the "Visual" effects (helps make sure that the ability are correctly positioned and parented)
    public Transform centreParticleSpawnPoint;
    public Transform exaustParticleSpawnPoint;

    [HideInInspector] public float powerupAmount = 0; //Variable that will get a value based on whichever ability is being used (for example the repair ability sets the powerupAmount to -20 to later remove 20 damage from the player etc)

    //Method that is used to spawn each ability depending on which ability it is (it takes in the ability that is being used to check its name for the correct behaviour!!)
    public void Activate(Ability thisAbility)
    {
        //If the ability name is repair, spawn it, rotate it (due to it not being correctly rotated when being spawned) and call the repair method
        if (thisAbility.Name == "Repair")
        {
            Debug.Log("This is the Repair ability");

            //Instantiate a copy of the powerup and store it in a new Gameobject variable called clondedPowerup
            GameObject clondedPowerup = Instantiate(particleEffect, centreParticleSpawnPoint.transform.position, centreParticleSpawnPoint.rotation, centreParticleSpawnPoint.transform) as GameObject;

            clondedPowerup.transform.Rotate(-90, 90, 0); //Setting the correct rotation for the repair effect

            clondedPowerup.GetComponent<RepairBehaviour>().RepairDamage(powerupAmount); //Calls the RepairDamage method and passes in the powerupAmount variable (this can be moved to the actual prefab that is spawned later)
        }

        //If the ability name is shield, spawn it
        if (thisAbility.Name == "Shield")
        {
            Debug.Log("This is the Shield ability");

            //Instantiate a copy of the powerup and store it in a new rigidbody variable called clondedPowerup
            GameObject clondedPowerup = Instantiate(particleEffect, centreParticleSpawnPoint.transform.position, centreParticleSpawnPoint.rotation, centreParticleSpawnPoint.transform) as GameObject;
        }

        //If the ability name is shield, spawn it
        if (thisAbility.Name == "Teleport")
        {
            Debug.Log("This is the Teleport ability");

            //Instantiate a copy of the powerup and store it in a new rigidbody variable called clondedPowerup
            GameObject clondedPowerup = Instantiate(particleEffect, centreParticleSpawnPoint.transform.position, centreParticleSpawnPoint.rotation, centreParticleSpawnPoint.transform) as GameObject;
        }

        //If the ability name is shield, spawn it and set the boost amount to the powerupAmount
        if (thisAbility.Name == "Boost")
        {
            Debug.Log("This is the Boost ability");

            //Instantiate a copy of the powerup and store it in a new rigidbody variable called clondedPowerup
            GameObject clondedPowerup = Instantiate(particleEffect, exaustParticleSpawnPoint.transform.position, exaustParticleSpawnPoint.rotation, exaustParticleSpawnPoint.transform) as GameObject;

            clondedPowerup.GetComponent<BoostBehaviour>().boostAmount = powerupAmount; //Set the boostAmount to the powerAmount
        }

        //If the ability name is Flamethrower, spawn it, rotate it (due to it not being correctly rotated when being spawned) and call the repair method
        if (thisAbility.Name == "Flamethrower")
        {
            Debug.Log("This is the Flamethrower ability");

            //Instantiate a copy of the powerup and store it in a new rigidbody variable called clondedPowerup
            GameObject clondedPowerup = Instantiate(particleEffect, exaustParticleSpawnPoint.transform.position, exaustParticleSpawnPoint.rotation, exaustParticleSpawnPoint.transform) as GameObject;

            clondedPowerup.transform.Rotate(0, 90, 90); //Setting the correct rotation for the Flamethrower effect

            clondedPowerup.GetComponentInChildren<FlamethrowerBehaviour>().damageAmount = powerupAmount; //Set the damageAmount to the powerAmount
        }
    }
}
