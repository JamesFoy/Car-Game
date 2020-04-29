using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShootTriggerable : MonoBehaviour
{
    [HideInInspector] public Rigidbody projectile;
    public Transform projectileSpawn;
    [HideInInspector] public float projectileForce = 250f;

    public void Launch()
    {
        //Instantiate a copy of the projectile and store it in a new rigidbody variable called clonedProjectile
        Rigidbody clonedProjectile = Instantiate(projectile, projectileSpawn.position, transform.rotation) as Rigidbody;

        //Add force to the instantiated bullet, pushing it forward away from the projectileSpawn loacation, using projectile force for how hard
        clonedProjectile.AddForce(projectileSpawn.transform.forward * projectileForce);
    }
}
