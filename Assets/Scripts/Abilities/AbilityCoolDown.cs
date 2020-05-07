using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Script that is used as the central point for the ability system (Script is used for UI setup, initalizing each ability and checking if it is ready to be triggered etc)
public class AbilityCoolDown : MonoBehaviour
{
    public bool canTriggerAbility = true; //Bool for checking if the ability can be used (Can add in a cooldown setup with this if we want multiple ability uses in the future etc)

    [SerializeField] private Ability ability1; //Variable holding the first ability
    [SerializeField] private Ability ability2; //Variable holding the second ability

    public GameObject weaponHolder1; //is the object that has the ability function script attached
    public GameObject weaponHolder2; //is the object that has the ability function script attached

    private Image myButtonImage; //Reference to the image object that will be used to display the correct icon for each ability

    private AudioSource abilitySource; //Reference to the audioSource on the UI gameObject (This is used to play an abilities sound effect when using it)

    PowerupAbility powerupAttack;
    PowerupAbility powerupDefense;

    ProjectileAbility projectileAttack;
    ProjectileAbility projectileDefense;

    //Method for initializing the the first ability (Takes in the first selected ability and weapon holder gameobject to assing the ability too)
    public void Initialize1(Ability selectedAbility1, GameObject weaponHolder1)
    {
        ability1 = selectedAbility1; //Sets the ability 1 varabile to the selected ability passed into the method when called
        myButtonImage = GetComponent<Image>(); //Gets the reference to the UI image component on the UI (This can probaly be done at start etc)
        abilitySource = GetComponent<AudioSource>(); //Gets the reference to the audioSource component on the UI (This can probaly be done at start etc)
        myButtonImage.sprite = ability1.Sprite; //Sets the image to the ability.Sprite reference (This is taken from the passed ability.. NOTE: initialize 2 also does this however it doesnt matter which ability is chosen as they have the same icon!!!!)
        ability1.Initialize(weaponHolder1); //This will Initialize the ability to use the correct weaponHolder script attached to the passed weapon holder GameObject (This will set the abilities variables like prefabs to spawn etc. to the Triggerable scripts!!)
        AbilityReady(); //Sets the bool of canTriggerAbility to true, meaning that the player is now able use use the ability since setup it complete
    }

    //Method for initializing the the second ability (Takes in the second selected ability and weapon holder gameobject to assing the ability too)
    public void Initialize2(Ability selectedAbility2, GameObject weaponHolder2)
    {
        ability2 = selectedAbility2; //Sets the ability 2 varabile to the selected ability passed into the method when called
        myButtonImage = GetComponent<Image>(); //Gets the reference to the UI image component on the UI (This can probaly be done at start etc)
        abilitySource = GetComponent<AudioSource>(); //Gets the reference to the audioSource component on the UI (This can probaly be done at start etc)
        myButtonImage.sprite = ability2.Sprite; //Sets the image to the ability.Sprite reference (This is taken from the passed ability.. NOTE: initialize 1 also does this however it doesnt matter which ability is chosen as they have the same icon!!!!)
        ability2.Initialize(weaponHolder2); //This will Initialize the ability to use the correct weaponHolder script attached to the passed weapon holder GameObject (This will set the abilities variables like prefabs to spawn etc. to the Triggerable scripts!!)
        AbilityReady(); //Sets the bool of canTriggerAbility to true, meaning that the player is now able use use the ability since setup it complete
    }

    public void InitializeAbility(AbilityHolder abilityTypeChosen)
    {
        List<string> powerupAbilities = new List<string> { "NanoBotAbility", "ChargeAbility", "TeleportAbility" }; //Creates a list of names that powerup abilities use
        List<string> projectileAbilities = new List<string> { "ExplosiveAbility", "ElectricAbility" };//Creates a list of names that projectile abilities use

        if (powerupAbilities.Contains(abilityTypeChosen.abilityName))
        {
            powerupAttack = abilityTypeChosen.powerupAbilities[0]; //Sets powerup 1 to the first ability from the ability holder chosen 
            powerupDefense = abilityTypeChosen.powerupAbilities[1]; //Sets powerup 2 to the second ability from the ability holder chosen
            Initialize1(powerupAttack, weaponHolder1); //Initalize the powerup and set its weapon holder
            Initialize2(powerupDefense, weaponHolder2); //Initalize the powerup and set its weapon holder
        }
        else if (projectileAbilities.Contains(abilityTypeChosen.abilityName))
        {
            projectileAttack = abilityTypeChosen.projectileAbilities[0]; //Sets projectile 1 to the first ability from the ability holder chosen 
            projectileDefense = abilityTypeChosen.projectileAbilities[1]; //Sets projectile 2 to the second ability from the ability holder chosen 
            Initialize1(projectileAttack, weaponHolder1); //Initalize the powerup and set its weapon holder
            Initialize2(projectileDefense, weaponHolder2); //Initalize the powerup and set its weapon holder
        }

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
            abilitySource.clip = ability1.Sound; //Sets the audio clip to play to the abilities sound effect (This can be null)
            abilitySource.Play(); //Plays the audioClip set to the UI audio Source
            ability1.TriggerAbility(deployStyle); //Triggers the ability based on the attack style
            myButtonImage.sprite = null; //removes the icon from the UI image
            canTriggerAbility = false; //Sets the canTriggerAbility to false
            this.gameObject.SetActive(false); //makes the active state of the gameObject attached to this script to false (Removes blank UI image and also helps make sure nothing can be activated)
        }

        //if the defense style is pressed
        else if (deployStyle == AbilityDeployModes.DeployStyle.Defense)
        {
            abilitySource.clip = ability2.Sound; //Sets the audio clip to play to the abilities sound effect (This can be null)
            abilitySource.Play(); //Plays the audioClip set to the UI audio Source
            ability2.TriggerAbility(deployStyle); //Triggers the ability based on the attack style
            myButtonImage.sprite = null; //removes the icon from the UI image
            canTriggerAbility = false; //Sets the canTriggerAbility to false
            this.gameObject.SetActive(false); //makes the active state of the gameObject attached to this script to false (Removes blank UI image and also helps make sure nothing can be activated)
        }
    }
}
