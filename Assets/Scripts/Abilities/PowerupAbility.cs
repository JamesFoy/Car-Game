using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Powerup Ability")]
public class PowerupAbility : Ability
{
    public GameObject particleEffect;
    public float powerupEffectAmount;

    private PowerupTriggerable Launcher;

    //script that provides a funciton implementation
    public override void Initialize(GameObject obj)
    {
        //implementation of script
        Launcher = obj.GetComponent<PowerupTriggerable>();
        Launcher.particleEffect = particleEffect;
        Launcher.powerupAmount = powerupEffectAmount;
    } 

    public override void TriggerAbility(float abilityNumber)
    {
        Launcher.Activate(this);
    }
}
