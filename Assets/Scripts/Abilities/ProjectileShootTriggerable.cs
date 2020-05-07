using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Script for spawning a projectile once a projectile ability is triggred elsewhere
public class ProjectileShootTriggerable : MonoBehaviour
{
    [HideInInspector] public Rigidbody projectile; //Creates a Rigidbody variable that will be assigned from the scriptable object ability

    public Transform projectileSpawn; //Variable to set the fire point of the projectile (Fire 1 is used for forward facing projectile and Fire 2 is for backward facing projectiles)

    [HideInInspector] public float projectileForce = 250f; //Sets a default projectileForce variable (this is overriden by the scriptable objects ability varaible)

    //Method that is used to spawn each projectile
    public void Launch()
    {
        //Instantiate a copy of the projectile and store it in a new rigidbody variable called clonedProjectile
        Rigidbody clonedProjectile = Instantiate(projectile, projectileSpawn.position, transform.rotation) as Rigidbody;

        //Add force to the instantiated bullet, pushing it forward away from the projectileSpawn loacation, using projectile force for how hard
        clonedProjectile.AddForce(projectileSpawn.transform.forward * projectileForce);
    }
}
