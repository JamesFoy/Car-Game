using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for creating a new powerup ability which inherits from the ability scriptable object
[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Powerup Ability")]
public class PowerupAbility : Ability
{
    public GameObject particleEffect; //Variable for containing the prefab for the powerup (This will be the "visual" for the powerup in the form of a prefab object)

    public float powerupEffectAmount; //Varibale for setting a powerup effect amount (This is used to do things like set an amount of speed, amount to repair etc)

    private PowerupTriggerable Launcher; //Variable that will contain the powerupTriggerable script

    //This is the override method for Initalizeing the ability (This takes a GameObject called obj.... this is where the WeaponHolders are passed in to access the correct script etc.)
    public override void Initialize(GameObject obj)
    {
        Launcher = obj.GetComponent<PowerupTriggerable>(); //Sets the launcher variable to the PowerupTriggerable script on the passed gameObject
        Launcher.particleEffect = particleEffect; //This will set the launchers (WeaponHolder's PowerupTriggerable) particleEffect to the particle effect assigned to the scriptable object (Again this is the prefab that will be spawned when a ability is used)
        Launcher.powerupAmount = powerupEffectAmount; //This sets the launchers (WeaponHolder's PowerupTriggerable) powerupAmount to the amount set in the scriptable object
    } 

    //This method is used to trigger the ability (It takes in an attack style to deploy the correctly ability when used e.g. attack style will activate attack style ability)
    public override void TriggerAbility(AbilityDeployModes.DeployStyle style)
    {
        Launcher.Activate(this);
    }
}
