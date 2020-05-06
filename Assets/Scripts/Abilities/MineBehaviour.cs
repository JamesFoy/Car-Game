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
            carInfo.carStats.health += forceAmount;
            GameObject clonedExplosion = Instantiate(explosion, transform.position, transform.rotation);
            rigidbody = other.gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.up * forceAmount, ForceMode.Impulse);
            Destroy(this.gameObject);
        }
    }
}
