using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script that is used as the central point for the ability system (Script is used for UI setup, initalizing each ability and checking if it is ready to be triggered etc)
public class AbilityInitializer : MonoBehaviour
{
    public bool canTriggerAbility = true; //Bool for checking if the ability can be used (Can add in a cooldown setup with this if we want multiple ability uses in the future etc)

    [SerializeField] private Ability ability1; //Variable holding the first ability
    [SerializeField] private Ability ability2; //Variable holding the second ability

    public GameObject weaponHolder1; //is the object that has the ability function script attached
    public GameObject weaponHolder2; //is the object that has the ability function script attached

    public AbilityUI abilityUI; // This is the UI object for this instance

    public void InitializeAbility(AbilitySet abilitySet)
    {
        List<string> powerupAbilities = new List<string> { "NanoBotAbility", "ChargeAbility", "TeleportAbility" }; //Creates a list of names that powerup abilities use
        List<string> projectileAbilities = new List<string> { "ExplosiveAbility", "ElectricAbility" };//Creates a list of names that projectile abilities use

        if (powerupAbilities.Contains(abilitySet.abilityName))
        {
            InitializeStyle(abilitySet.powerupAbilities[0], weaponHolder1, AbilityDeployModes.DeployStyle.Attack); //Initalize the powerup and set its weapon holder
            InitializeStyle(abilitySet.powerupAbilities[1], weaponHolder2, AbilityDeployModes.DeployStyle.Defense); //Initalize the powerup and set its weapon holder
        }
        else if (projectileAbilities.Contains(abilitySet.abilityName))
        {
            InitializeStyle(abilitySet.projectileAbilities[0], weaponHolder1, AbilityDeployModes.DeployStyle.Attack); //Initalize the powerup and set its weapon holder
            InitializeStyle(abilitySet.projectileAbilities[1], weaponHolder2, AbilityDeployModes.DeployStyle.Defense); //Initalize the powerup and set its weapon holder
        }

    }
    private void InitializeStyle(Ability selectedAbility, GameObject weaponHolder, AbilityDeployModes.DeployStyle style)
    {
        abilityUI.AssignSprite(selectedAbility.Sprite);
        selectedAbility.Initialize(weaponHolder);
        if (style == AbilityDeployModes.DeployStyle.Attack)
        {
            ability1 = selectedAbility;
        }
        else if (style == AbilityDeployModes.DeployStyle.Defense)
        {
            ability2 = selectedAbility;
        }
        AbilityReady();
    }
    //Method for setting the bool of canTriggerAbility to true (used once Initialize is done)
    private void AbilityReady()
    {
        canTriggerAbility = true;
    }
    //Method used to check which button is being used and perform certain actions when pressed
    public void ButtonTriggered(AbilityDeployModes.DeployStyle deployStyle)
    {
        //if the attack style is pressed
        if (deployStyle == AbilityDeployModes.DeployStyle.Attack)
        {
            TriggerAbilitySequence(ability1, deployStyle);
        }
        //if the defense style is pressed
        else if (deployStyle == AbilityDeployModes.DeployStyle.Defense)
        {
            TriggerAbilitySequence(ability2, deployStyle);
        }
    }
    private void TriggerAbilitySequence(Ability ability, AbilityDeployModes.DeployStyle deployStyle)
    {
        abilityUI.PlaySoundEffect(ability.Sound);
        ability.TriggerAbility(deployStyle); //Triggers the ability based on the attack style
        abilityUI.AssignSprite(null); //removes the icon from the UI image
        canTriggerAbility = false; //Sets the canTriggerAbility to false
        gameObject.SetActive(false); //makes the active state of the gameObject attached to this script to false (Removes blank UI image and also helps make sure nothing can be activated)
    }
}
