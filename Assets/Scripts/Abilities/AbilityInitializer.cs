﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script that is used as the central point for the ability system (Script is used for UI setup, initalizing each ability and checking if it is ready to be triggered etc)
public class AbilityInitializer : MonoBehaviour
{
    #region Variable Setup
    [SerializeField] bool canTriggerAbility = true; //Bool for checking if the ability can be used (Can add in a cooldown setup with this if we want multiple ability uses in the future etc)

    [SerializeField] Ability abilityAttack; //Variable holding the first ability
    [SerializeField] Ability abilityDefense; //Variable holding the second ability

    [SerializeField] GameObject weaponHolderAttack; //is the object that has the ability function script attached
    [SerializeField] GameObject weaponHolderDefense; //is the object that has the ability function script attached

    [SerializeField] AbilityUI abilityUI; // This is the UI object for this instance

    List<AbilitySet> possibleAbilitySets; //manually add the list of possible powerups to choose from
    private AbilitySet chosenAbilitySet;
    #endregion

    #region Ability Selection and Initialization
    public void RandomPickupSelector()
    {
        if (possibleAbilitySets != null)
        {
            chosenAbilitySet = possibleAbilitySets[Random.Range(0, possibleAbilitySets.Count)]; //chooses a random ability set from a list of all ability sets available
            VerifyAbilitySetName(chosenAbilitySet);
        }
    }
    private void VerifyAbilitySetName(AbilitySet abilitySet)
    {
        List<string> powerupAbilities = new List<string> { "NanoBotAbility", "ChargeAbility", "TeleportAbility" }; //Creates a list of names that powerup abilities use
        List<string> projectileAbilities = new List<string> { "ExplosiveAbility", "ElectricAbility" };//Creates a list of names that projectile abilities use
        List<string> teleportAbilities = new List<string> { "TeleportAbility" };

        if (powerupAbilities.Contains(abilitySet.abilityName))
        {
            InitialiseAbility(abilitySet.powerupAbilities[0], weaponHolderAttack, AbilityDeployModes.DeployStyle.Attack); //Initalize the powerup and set its weapon holder
            InitialiseAbility(abilitySet.powerupAbilities[1], weaponHolderDefense, AbilityDeployModes.DeployStyle.Defense); //Initalize the powerup and set its weapon holder
        }
        else if (projectileAbilities.Contains(abilitySet.abilityName))
        {
            InitialiseAbility(abilitySet.projectileAbilities[0], weaponHolderAttack, AbilityDeployModes.DeployStyle.Attack); //Initalize the powerup and set its weapon holder
            InitialiseAbility(abilitySet.projectileAbilities[1], weaponHolderDefense, AbilityDeployModes.DeployStyle.Defense); //Initalize the powerup and set its weapon holder
        }
        else if (teleportAbilities.Contains(abilitySet.abilityName))
        {
            //InitialiseAbility(abilitySet.teleportAbilities[0], weaponHolderAttack, AbilityDeployModes.DeployStyle.Attack);
            //InitialiseAbility(abilitySet.teleportAbilities[0], weaponHolderDefense, AbilityDeployModes.DeployStyle.Defense);
        }
    }
    private void InitialiseAbility(Ability selectedAbility, GameObject weaponHolder, AbilityDeployModes.DeployStyle style)
    {
        selectedAbility.Initialize(weaponHolder);
        if (style == AbilityDeployModes.DeployStyle.Attack)
        {
            abilityAttack = selectedAbility;
        }
        else if (style == AbilityDeployModes.DeployStyle.Defense)
        {
            abilityDefense = selectedAbility;
        }

        if (abilityUI != null)
        {
            abilityUI.AssignSprite(selectedAbility.Sprite);
            abilityUI.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("UI Not Active");
        }
        // mark this ability as initialized and ready to deploy
        if (selectedAbility != null)
        {
            canTriggerAbility = true;
        }
        else
        {
            canTriggerAbility = false;
        }
    }
    #endregion
    #region Ability Triggering and Deployment
    public void TriggerAbilitySequence(AbilityDeployModes.DeployStyle style)
    {
        if (canTriggerAbility) //Check if the ability can be fired
        {
            //if the attack style is pressed
            if (style == AbilityDeployModes.DeployStyle.Attack)
            {
                AbilityDeploy(abilityAttack, style);
            }
            //if the defense style is pressed
            else if (style == AbilityDeployModes.DeployStyle.Defense)
            {
                AbilityDeploy(abilityDefense, style);   
            }
        }
    }
    private void AbilityDeploy(Ability ability, AbilityDeployModes.DeployStyle deployStyle)
    {
        if (ability != null)
        {
            ability.TriggerAbility(deployStyle); //Triggers the ability based on the attack style
        }

        if (abilityUI != null && abilityUI.gameObject.activeInHierarchy)
        {
            abilityUI.PlaySoundEffect(ability.Sound);
            abilityUI.AssignSprite(null); //removes the icon from the UI image
            abilityUI.gameObject.SetActive(false); //makes the active state of the gameObject attached to this script to false (Removes blank UI image and also helps make sure nothing can be activated)
        }

        //mark the ability as used, and stop more triggering events in TriggerAbilitySequence
        canTriggerAbility = false;
    }
    #endregion
    public void LinkAbilityUIToACar(AbilityUI abilityUI)
    {
        this.abilityUI = abilityUI;
    }
    public void DefineAbilitySets(List<AbilitySet> abilitySets)
    {
        possibleAbilitySets = abilitySets;
    }

    public bool CanThisCarTriggerAbility()
    {
        return canTriggerAbility;
    }
}