using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    public GameObject explosion;
    CarInfo carInfo;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            carInfo = other.gameObject.GetComponent<CarInfo>();

            if (!carInfo.carStats.isShieldEnabled)
            {
                carInfo.carStats.health += 10;
            }
        }

        GameObject clonedExplosion = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
