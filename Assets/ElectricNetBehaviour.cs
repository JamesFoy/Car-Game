using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricNetBehaviour : MonoBehaviour
{
    public GameObject electricEffect;
    Vector3 offset = new Vector3(0, 1.6f, 0);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject clonedEffect = Instantiate(electricEffect, other.transform.position + offset, other.transform.rotation, other.transform);
            clonedEffect.transform.Rotate(0, 90, 0);
        }

        Destroy(this.gameObject);
    }
}
