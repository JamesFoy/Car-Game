using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for creating a new projectile ability which inherits from the ability scriptable object
[CreateAssetMenu (menuName = "Scriptable Objects/Abilities/Projectile Ability")]
public class ProjectileAbility : Ability
{
    public bool useGravity; //bool to check if the ability should have gravity effect it

    public float projectileForce = 1; //Variable that will have its value changed in the editor on the scriptable object that is created

    public Rigidbody projectile; //Variable to assign a Prefab for the ability to use (This is the thing that will be shot!!)

    private ProjectileShootTriggerable launcher; //Variable that will contain the ProjectileShootTriggerable script

    //This is the override method for Initalizeing the ability (This takes a GameObject called obj.... this is where the WeaponHolders are passed in to access the correct script etc.)
    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<ProjectileShootTriggerable>();//Sets the launcher variable to the ProjectileShootTriggerable script on the passed gameObject

        launcher.projectileForce = projectileForce; //This sets the launchers projectile force to the abilities projectile force variable

        launcher.projectile = projectile; //This will set the launchers (WeaponHolder's ProjectileShootTriggerable) projectile to the projectile assigned to the scriptable object (Again this is the prefab that will be spawned when a ability is used)

        launcher.projectile.useGravity = useGravity; //This sets the projectile's gravity bool to the abilities useGravity bool
    }

    //This method is used to trigger the ability (For example, it takes in an attack style to deploy the correctly ability when used e.g. attack style will activate attack style ability)
    public override void TriggerAbility(AbilityDeployModes.DeployStyle style)
    {
        launcher.Launch();
    }
}
