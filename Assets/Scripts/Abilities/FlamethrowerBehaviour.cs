using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerBehaviour : MonoBehaviour
{
    [HideInInspector]public float damageAmount;
    CarInfo carInfo;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            carInfo = other.GetComponent<CarInfo>();
            carInfo.carStats.health -= damageAmount;
        }
    }
}
