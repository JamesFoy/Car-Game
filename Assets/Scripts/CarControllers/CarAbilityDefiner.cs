using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for setting up and controlling ability behaviour on the player
public class CarAbilityDefiner : MonoBehaviour
{
    public AbilityInitializer abilityInitializer; //Reference to the UI ability (needs to be called when a button is pressed)
    public GameObject abilityUIImage; //Reference to the game object that contains the ability UI (makes sure that it only works when activated, activation when picking up powerup occurs etc)

    private void Start()
    {
        abilityInitializer = GetComponent<AbilityInitializer>();
        abilityUIImage.SetActive(false); //Sets the ability UI gameobject active state to false (makes sure that any input from the player doesnt cause issues due to no ability being setup)
    }


    //triggers the ability which is set to a specifc button (in this case attack style is RB button or E/NumPad Enter key on keyboard and defend attack style is LB button or Q/NumPad Period on keyboard)
    public void AbilityTrigger(AbilityDeployModes.DeployStyle style)
    {
        if (abilityInitializer.canTriggerAbility && abilityUIImage.activeSelf) //Check if the ability can be fired and if the game object is active in the scene
        {
            abilityInitializer.ButtonTriggered(style); //Trigger ability
        }
    }
}