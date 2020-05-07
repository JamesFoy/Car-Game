using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for setting up and controlling ability behaviour on the player
public class CarAbility : MonoBehaviour
{
    public AbilityCoolDown ability; //Reference to the UI ability (needs to be called when a button is pressed)
    public GameObject abilityUIImage; //Reference to the game object that contains the ability UI (makes sure that it only works when activated, activation when picking up powerup occurs etc)

    public List<AbilitySet> possibleAbilitySets; //Creating a list of possible abilites/powerups
    public AbilitySet chosenAbilitySet; //Varibale that will hold the ability chosen from the list above

    //Variables containing the powerup abilities
    public PowerupAbility powerupAttack;
    public PowerupAbility powerupDefense;

    //Variables containing the projectile abilities
    public ProjectileAbility projectileAttack;
    public ProjectileAbility projectileDefense;

    AbilityCoolDown abilityCoolDown; //Reference to the AbilityCoolDown script

    private void Start()
    {
        abilityUIImage.SetActive(false); //Sets the ability UI gameobject active state to false (makes sure that any input from the player doesnt cause issues due to no ability being setup)
    }

    //THIS METHOD IS CALLED USEING THE PICKUP GAME EVENT (EVENT IS RAISED WHENEVER A PLAYER COLLIDES WITH A PICKUP)
    //Method used to pick a random ability from the ability list (also in the furture can add a timer and play an animation on the ability icon to show a roulette sort of randomising)
    public void RandomPickupGenerator()
    {
        chosenAbilitySet = possibleAbilitySets[Random.Range(0, possibleAbilitySets.Count)]; //chooses a random ability set from a list of all ability sets available
        abilityCoolDown = abilityUIImage.GetComponent<AbilityCoolDown>(); //Sets the location of the ability cooldown script so that abilities can be setup

        abilityCoolDown.InitializeAbility(chosenAbilitySet);

        abilityUIImage.SetActive(true); //Sets the ability HUD game object to active so the ability can be used
    }

    //triggers the ability which is set to a specifc button (in this case attack style is RB button or E/NumPad Enter key on keyboard and defend attack style is LB button or Q/NumPad Period on keyboard)
    public void AbilityTrigger(AbilityDeployModes.DeployStyle style)
    {
        if (ability.canTriggerAbility && abilityUIImage.activeSelf) //Check if the ability can be fired and if the game object is active in the scene
        {
            ability.ButtonTriggered(style); //Trigger ability
        }
    }
}