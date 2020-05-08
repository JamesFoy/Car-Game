using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    CarController carController;

    private void Start()
    {
        carController = GetComponentInParent<CarController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Track"))
        {
            carController.SetRigidbodyValues(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Track"))
        {
            carController.SetRigidbodyValues(true);
        }
    }
}
