using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability
{
    public float projectileForce = 1;
    public Rigidbody projectile;

    private ProjectileShootTriggerable launcher;

    //script that provides a funciton implementation
    public override void Initialize(GameObject obj)
    {
        //implementation of script
        launcher = obj.GetComponent<ProjectileShootTriggerable>();
        launcher.projectileForce = projectileForce;
        launcher.projectile = projectile;
    }

    public override void TriggerAbility()
    {
        //make the ability activate
        launcher.Launch();
    }
}
