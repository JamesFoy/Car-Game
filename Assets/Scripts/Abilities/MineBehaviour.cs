using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviour : MonoBehaviour
{
    public GameObject explosion;
    public float forceAmount;
    Rigidbody rigidbody;
    CarInfo carInfo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            carInfo = other.gameObject.GetComponent<CarInfo>();

            if (!carInfo.carStats.isShieldEnabled)
            {
                carInfo.carStats.health += forceAmount;
                rigidbody = other.gameObject.GetComponent<Rigidbody>();
                rigidbody.AddForce(transform.up * forceAmount, ForceMode.Impulse);
            }

            GameObject clonedExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
