using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script that contains all of the shield behaviour
public class ShieldBehaviour : MonoBehaviour
{
    //References to the collsion scripts
    AddDamageBasedOnForce addDamage;
    AddForceBasedOnHealth addForce;

    //When the shield ability is enabled/spawned it will prevent damage for the player with the shield active
    private void OnEnable()
    {
        addDamage = GetComponentInParent<AddDamageBasedOnForce>();
        addForce = GetComponentInParent<AddForceBasedOnHealth>();

        addDamage.enabled = false;
        addForce.enabled = false;
    }

    //When the shield is destroyed, it will enable the scripts for damage and collision (This can be changed to on disable when object pooling is setup)
    private void OnDestroy()
    {
        addDamage.enabled = true;
        addForce.enabled = true;
    }
}
