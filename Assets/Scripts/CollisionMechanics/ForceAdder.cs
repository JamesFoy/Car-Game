using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAdder : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.down * 100);
        GetComponent<Rigidbody>().AddForce(Vector3.left * 100);
    }
}
