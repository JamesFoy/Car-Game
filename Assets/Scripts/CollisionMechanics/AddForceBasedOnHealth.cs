using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceBasedOnHealth : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] CarDataSO car;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
    }
}