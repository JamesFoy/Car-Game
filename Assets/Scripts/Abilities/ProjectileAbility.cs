using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Scriptable Objects/Abilities/Projectile Ability")]
public class ProjectileAbility : Ability
{
    public bool useGravity;
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
        launcher.projectile.useGravity = useGravity;
    }

    public override void TriggerAbility(float abilityNumber)
    {
        launcher.Launch();
    }
}
