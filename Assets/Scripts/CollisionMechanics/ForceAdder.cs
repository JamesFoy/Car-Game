using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAdder : MonoBehaviour
{
    public float speed;
    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.down * 100 * speed);
        GetComponent<Rigidbody>().AddForce(Vector3.right * 100 * speed);
    }
}
