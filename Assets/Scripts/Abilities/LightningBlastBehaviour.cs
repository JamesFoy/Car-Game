using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBlastBehaviour : MonoBehaviour
{
    public GameObject explosion;
    public GameObject electricEffect;
    Vector3 offset = new Vector3(0, 1.6f, 0);

    CarInfo carInfo;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            carInfo = other.gameObject.GetComponent<CarInfo>();

            if (!carInfo.carStats.isShieldEnabled)
            {
                GameObject clonedEffect = Instantiate(electricEffect, other.transform.position + offset, other.transform.rotation, other.transform);
                clonedEffect.transform.Rotate(0, 90, 0);
            }
        }

        GameObject clonedExplosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
