using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityCoolDown : MonoBehaviour
{
    public bool canTriggerAbility = true;

    [SerializeField] private Ability ability;
    [SerializeField] private GameObject weaponHolder; //is the object that has the ability function script attached
    private Image myButtonImage;
    private AudioSource abilitySource;


    public void Initialize(Ability selectedAbility, GameObject weaponHolder)
    {
        ability = selectedAbility;
        myButtonImage = GetComponent<Image>();
        abilitySource = GetComponent<AudioSource>();
        myButtonImage.sprite = ability.Sprite;
        ability.Initialize(weaponHolder);
        AbilityReady();
    }

    private void AbilityReady()
    {
        canTriggerAbility = true;
    }


    public void ButtonTriggered()
    {
        abilitySource.clip = ability.Sound;
        abilitySource.Play();
        ability.TriggerAbility();
        myButtonImage.sprite = null;
        canTriggerAbility = false;
        this.gameObject.SetActive(false);
    }
}
