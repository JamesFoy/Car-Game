using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script that contains all of the shield behaviour
public class ShieldBehaviour : MonoBehaviour
{
    //References to the collsion scripts
    AddDamageBasedOnForce addDamage;
    AddForceBasedOnHealth addForce;
    CarInfo carInfo;

    //When the shield ability is enabled/spawned it will prevent damage for the player with the shield active
    private void OnEnable()
    {
        carInfo = GetComponentInParent<CarInfo>();
        addDamage = GetComponentInParent<AddDamageBasedOnForce>();
        addForce = GetComponentInParent<AddForceBasedOnHealth>();

        addDamage.enabled = false;
        addForce.enabled = false;

        carInfo.carStats.isShieldEnabled = true;
    }

    //When the shield is destroyed, it will enable the scripts for damage and collision (This can be changed to on disable when object pooling is setup)
    private void OnDestroy()
    {
        carInfo.carStats.isShieldEnabled = false;

        addDamage.enabled = true;
        addForce.enabled = true;
    }
}
