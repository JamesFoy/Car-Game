using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityCoolDown : MonoBehaviour
{
    public bool canTriggerAbility = true;

    [SerializeField] private Ability ability1;
    [SerializeField] private Ability ability2;
    public GameObject weaponHolder1; //is the object that has the ability function script attached
    public GameObject weaponHolder2; //is the object that has the ability function script attached
    private Image myButtonImage;
    private AudioSource abilitySource;

    public void Initialize1(Ability selectedAbility1, GameObject weaponHolder1)
    {
        ability1 = selectedAbility1;
        myButtonImage = GetComponent<Image>();
        abilitySource = GetComponent<AudioSource>();
        myButtonImage.sprite = ability1.Sprite;
        ability1.Initialize(weaponHolder1);
        AbilityReady();
    }

    public void Initialize2(Ability selectedAbility2, GameObject weaponHolder2)
    {
        ability2 = selectedAbility2;
        myButtonImage = GetComponent<Image>();
        abilitySource = GetComponent<AudioSource>();
        myButtonImage.sprite = ability2.Sprite;
        ability2.Initialize(weaponHolder2);
        AbilityReady();
    }

    private void AbilityReady()
    {
        canTriggerAbility = true;
    }


    public void ButtonTriggered(AbilityDeployModes.DeployStyle triggerType)
    {
        if (triggerType == AbilityDeployModes.DeployStyle.Attack)
        {
            abilitySource.clip = ability1.Sound;
            abilitySource.Play();
            ability1.TriggerAbility(0);
            myButtonImage.sprite = null;
            canTriggerAbility = false;
            this.gameObject.SetActive(false);
        }
        else if (triggerType == AbilityDeployModes.DeployStyle.Defense)
        {
            abilitySource.clip = ability2.Sound;
            abilitySource.Play();
            ability2.TriggerAbility(1);
            myButtonImage.sprite = null;
            canTriggerAbility = false;
            this.gameObject.SetActive(false);
       }
    }
}
