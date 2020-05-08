using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script that is used as the central point for the ability system (Script is used for UI setup, initalizing each ability and checking if it is ready to be triggered etc)
public class AbilityInitializer : MonoBehaviour
{
    [SerializeField] bool canTriggerAbility = true; //Bool for checking if the ability can be used (Can add in a cooldown setup with this if we want multiple ability uses in the future etc)

    [SerializeField] private Ability abilityAttack; //Variable holding the first ability
    [SerializeField] private Ability abilityDefense; //Variable holding the second ability

    [SerializeField] GameObject weaponHolderAttack; //is the object that has the ability function script attached
    [SerializeField] GameObject weaponHolderDefense; //is the object that has the ability function script attached

    [SerializeField] AbilityUI abilityUI; // This is the UI object for this instance

    public List<AbilitySet> possibleAbilitySets; //manually add the list of possible powerups to choose from
    private AbilitySet chosenAbilitySet;

    public void RandomPickupSelector()
    {
        chosenAbilitySet = possibleAbilitySets[Random.Range(0, possibleAbilitySets.Count)]; //chooses a random ability set from a list of all ability sets available
        InitializeAbility(chosenAbilitySet);
        abilityUI.gameObject.SetActive(true);
    }
    public void InitializeAbility(AbilitySet abilitySet)
    {
        List<string> powerupAbilities = new List<string> { "NanoBotAbility", "ChargeAbility", "TeleportAbility" }; //Creates a list of names that powerup abilities use
        List<string> projectileAbilities = new List<string> { "ExplosiveAbility", "ElectricAbility" };//Creates a list of names that projectile abilities use

        if (powerupAbilities.Contains(abilitySet.abilityName))
        {
            InitializeStyle(abilitySet.powerupAbilities[0], weaponHolderAttack, AbilityDeployModes.DeployStyle.Attack); //Initalize the powerup and set its weapon holder
            InitializeStyle(abilitySet.powerupAbilities[1], weaponHolderDefense, AbilityDeployModes.DeployStyle.Defense); //Initalize the powerup and set its weapon holder
        }
        else if (projectileAbilities.Contains(abilitySet.abilityName))
        {
            InitializeStyle(abilitySet.projectileAbilities[0], weaponHolderAttack, AbilityDeployModes.DeployStyle.Attack); //Initalize the powerup and set its weapon holder
            InitializeStyle(abilitySet.projectileAbilities[1], weaponHolderDefense, AbilityDeployModes.DeployStyle.Defense); //Initalize the powerup and set its weapon holder
        }

    }
    private void InitializeStyle(Ability selectedAbility, GameObject weaponHolder, AbilityDeployModes.DeployStyle style)
    {
        abilityUI.AssignSprite(selectedAbility.Sprite);
        selectedAbility.Initialize(weaponHolder);
        if (style == AbilityDeployModes.DeployStyle.Attack)
        {
            abilityAttack = selectedAbility;
        }
        else if (style == AbilityDeployModes.DeployStyle.Defense)
        {
            abilityDefense = selectedAbility;
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
            TriggerAbilitySequence(abilityAttack, deployStyle);
        }
        //if the defense style is pressed
        else if (deployStyle == AbilityDeployModes.DeployStyle.Defense)
        {
            TriggerAbilitySequence(abilityDefense, deployStyle);
        }
    }
    private void TriggerAbilitySequence(Ability ability, AbilityDeployModes.DeployStyle deployStyle)
    {
        abilityUI.PlaySoundEffect(ability.Sound);
        ability.TriggerAbility(deployStyle); //Triggers the ability based on the attack style
        abilityUI.AssignSprite(null); //removes the icon from the UI image
        canTriggerAbility = false; //Sets the canTriggerAbility to false
        abilityUI.gameObject.SetActive(false); //makes the active state of the gameObject attached to this script to false (Removes blank UI image and also helps make sure nothing can be activated)
    }
    public void AbilityTrigger(AbilityDeployModes.DeployStyle style)
    {
        if (canTriggerAbility && abilityUI.gameObject.activeSelf) //Check if the ability can be fired and if the game object is active in the scene
        {
            ButtonTriggered(style); //Trigger ability
        }
    }
}
