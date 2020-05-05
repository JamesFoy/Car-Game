using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Powerup Ability")]
public class PowerupAbility : Ability
{
    public GameObject particleEffect;
    public float powerupEffectAmount;

    private PowerupTriggerable launcher;

    //script that provides a funciton implementation
    public override void Initialize(GameObject obj)
    {
        //implementation of script
        launcher = obj.GetComponent<PowerupTriggerable>();
        launcher.particleEffect = particleEffect;
        launcher.powerupAmount = powerupEffectAmount;
    }

    public override void TriggerAbility()
    {
        //make the ability activate
        launcher.Activate();
    }
}
