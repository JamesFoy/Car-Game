using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDamageBasedOnForce : MonoBehaviour
{
    Rigidbody rb;
    CarStats car;
    int frames;
    float lastFrameTimeDeltaTime;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (car == null)
        {
            car = GetComponent<CarController>().carData;
        }
    }
    private void FixedUpdate()
    {
        if (frames < 1)
        {
            frames = 1;
            lastFrameTimeDeltaTime = Time.deltaTime;
        }
        frames--;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 originalForce = collision.impulse / lastFrameTimeDeltaTime;
            float forceMagnitude = originalForce.magnitude;
            float damageApplied = forceMagnitude * 0.01f;
            car.health += damageApplied;
            Debug.Log("damage applied: " + damageApplied);
        }
    }
}
