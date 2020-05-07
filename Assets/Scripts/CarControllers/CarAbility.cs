using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAbility : MonoBehaviour
{
    public AbilityCoolDown ability; //Reference to the UI ability (needs to be called when a button is pressed)
    public GameObject abilityUIImage; //Reference to the game object that contains the ability UI (makes sure that it only works when activated, activation when picking up powerup occurs etc)

    //Creating a list of possible abilites/powerups
    public List<AbilityHolder> possibleAbilities;
    public AbilityHolder abilityTypeChosen;
    public PowerupAbility powerup1;
    public PowerupAbility powerup2;
    public ProjectileAbility projectile1;
    public ProjectileAbility projectile2;
    AbilityCoolDown abilityCoolDown;


    private void Start()
    {
        abilityUIImage.SetActive(false);
    }

    //THIS METHOD IS CALLED USEING THE PICKUP GAME EVENT (EVENT IS RAISED WHENEVER A PLAYER COLLIDES WITH A PICKUP)
    //Method used to pick a random ability from the ability list, currently only the rocket will be chosen (also in the furture can add a timer and play an animation on the ability icon to show a roulette sort of randomising)
    public void RandomPickupGenerator()
    {
        abilityTypeChosen = possibleAbilities[UnityEngine.Random.Range(0, possibleAbilities.Count)];
        abilityCoolDown = abilityUIImage.GetComponent<AbilityCoolDown>();

        List<string> powerupAbilities = new List<string> { "NanoBotAbility", "ChargeAbility", "TeleportAbility" };
        List<string> projectileAbilities = new List<string> { "ExplosiveAbility", "ElectricAbility" };

        if (powerupAbilities.Contains(abilityTypeChosen.abilityName))
        {
            powerup1 = abilityTypeChosen.powerupAbilities[0];
            powerup2 = abilityTypeChosen.powerupAbilities[1];
            abilityCoolDown.Initialize1(powerup1, abilityCoolDown.weaponHolder1);
            abilityCoolDown.Initialize2(powerup2, abilityCoolDown.weaponHolder2);
        }
        else if (projectileAbilities.Contains(abilityTypeChosen.abilityName))
        {
            projectile1 = abilityTypeChosen.projectileAbilities[0];
            projectile2 = abilityTypeChosen.projectileAbilities[1];
            abilityCoolDown.Initialize1(projectile1, abilityCoolDown.weaponHolder1);
            abilityCoolDown.Initialize2(projectile2, abilityCoolDown.weaponHolder2);
        }

        abilityUIImage.SetActive(true); //Sets the ability HUD game object to active so the ability can be used
    }
    public void AbilityTrigger(AbilityDeployModes.DeployStyle style)
    {
        if (ability.canTriggerAbility && abilityUIImage.activeSelf) //Check if the ability can be fired and if the game object is active in the scene
        {
            ability.ButtonTriggered(style); //Trigger ability
        }
    }
}