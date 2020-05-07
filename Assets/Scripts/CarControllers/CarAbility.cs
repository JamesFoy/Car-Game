using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for setting up and controlling ability behaviour on the player
public class CarAbility : MonoBehaviour
{
    public AbilityCoolDown ability; //Reference to the UI ability (needs to be called when a button is pressed)
    public GameObject abilityUIImage; //Reference to the game object that contains the ability UI (makes sure that it only works when activated, activation when picking up powerup occurs etc)

    public List<AbilityHolder> possibleAbilities; //Creating a list of possible abilites/powerups
    public AbilityHolder abilityTypeChosen; //Varibale that will hold the ability chosen from the list above

    //Variables containing the powerup abilities
    public PowerupAbility powerup1;
    public PowerupAbility powerup2;

    //Variables containing the projectile abilities
    public ProjectileAbility projectile1;
    public ProjectileAbility projectile2;

    AbilityCoolDown abilityCoolDown; //Reference to the AbilityCoolDown script

    private void Start()
    {
        abilityUIImage.SetActive(false); //Sets the ability UI gameobject active state to false (makes sure that any input from the player doesnt cause issues due to no ability being setup)
    }

    //THIS METHOD IS CALLED USEING THE PICKUP GAME EVENT (EVENT IS RAISED WHENEVER A PLAYER COLLIDES WITH A PICKUP)
    //Method used to pick a random ability from the ability list (also in the furture can add a timer and play an animation on the ability icon to show a roulette sort of randomising)
    public void RandomPickupGenerator()
    {
        abilityTypeChosen = possibleAbilities[UnityEngine.Random.Range(0, possibleAbilities.Count)]; //Sets the ability chosen to a random choice of ability holders
        abilityCoolDown = abilityUIImage.GetComponent<AbilityCoolDown>(); //Sets the location of the ability cooldown script so that abilities can be setup

        List<string> powerupAbilities = new List<string> { "NanoBotAbility", "ChargeAbility", "TeleportAbility" }; //Creates a list of names that powerup abilities use
        List<string> projectileAbilities = new List<string> { "ExplosiveAbility", "ElectricAbility" };//Creates a list of names that projectile abilities use

        //Checks if the random ability holder contains a powerup ability with the name from the list created above
        if (powerupAbilities.Contains(abilityTypeChosen.abilityName))
        {
            powerup1 = abilityTypeChosen.powerupAbilities[0]; //Sets powerup 1 to the first ability from the ability holder chosen 
            powerup2 = abilityTypeChosen.powerupAbilities[1]; //Sets powerup 2 to the second ability from the ability holder chosen
            abilityCoolDown.Initialize1(powerup1, abilityCoolDown.weaponHolder1); //Initalize the powerup and set its weapon holder
            abilityCoolDown.Initialize2(powerup2, abilityCoolDown.weaponHolder2); //Initalize the powerup and set its weapon holder
        }
        //Checks if the random ability holder contains a projectile ability with the name from the list created above
        else if (projectileAbilities.Contains(abilityTypeChosen.abilityName))
        {
            projectile1 = abilityTypeChosen.projectileAbilities[0]; //Sets projectile 1 to the first ability from the ability holder chosen 
            projectile2 = abilityTypeChosen.projectileAbilities[1]; //Sets projectile 2 to the second ability from the ability holder chosen 
            abilityCoolDown.Initialize1(projectile1, abilityCoolDown.weaponHolder1); //Initalize the powerup and set its weapon holder
            abilityCoolDown.Initialize2(projectile2, abilityCoolDown.weaponHolder2); //Initalize the powerup and set its weapon holder
        }

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