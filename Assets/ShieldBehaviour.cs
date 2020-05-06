using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    AddDamageBasedOnForce addDamage;
    AddForceBasedOnHealth addForce;

    // Start is called before the first frame update
    private void OnEnable()
    {
        addDamage = GetComponentInParent<AddDamageBasedOnForce>();
        addForce = GetComponentInParent<AddForceBasedOnHealth>();
        addDamage.enabled = false;
        addForce.enabled = false;
    }

    private void OnDestroy()
    {
        addDamage.enabled = true;
        addForce.enabled = true;
    }
}
