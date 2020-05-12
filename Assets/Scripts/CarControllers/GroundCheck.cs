using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    CarController carController;
    float delayCheckTime = 0.8f;
    float timePassed;

    private void Start()
    {
        carController = GetComponentInParent<CarController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Track"))
        {
            Debug.Log("Entered new trigger");
            timePassed = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Track"))
        {
            Debug.Log("On track");
            timePassed += Time.deltaTime;
            carController.SetRigidbodyValues(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (delayCheckTime < timePassed && other.gameObject.CompareTag("Track"))
        {
            Debug.Log("Not on Track");
            carController.SetRigidbodyValues(true);
        }
    }
}
