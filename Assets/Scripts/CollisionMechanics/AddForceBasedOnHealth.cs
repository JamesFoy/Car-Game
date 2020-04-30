using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceBasedOnHealth : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] CarData car;
    int frames;
    float lastFrameTimeDeltaTime;
    public Vector3 VelocityOnLastFrame { get; private set; }

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
    void FixedUpdate()
    {
        if (frames < 1)
        {
            VelocityOnLastFrame = GetVelocity();
            frames = 1;
            lastFrameTimeDeltaTime = Time.deltaTime;
        }
        frames--;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 relativeVelocity = collision.gameObject.GetComponent<AddForceBasedOnHealth>().VelocityOnLastFrame - VelocityOnLastFrame;
            Debug.Log("relative velocity: " + relativeVelocity);

            Vector3 originalForce = collision.impulse / lastFrameTimeDeltaTime;
            if (Vector3.Angle(relativeVelocity, originalForce) > 90f)
            {
                originalForce = -originalForce;
            }
            Vector3 healthScaledForce = originalForce * car.health;
            rb.AddForce(healthScaledForce);
            DebugLogForces(collision, relativeVelocity, originalForce);
        }
    }
    Vector3 GetVelocity()
    {
        return rb.velocity;
    }
    void DebugLogForces(Collision collision, Vector3 forceToDraw, Vector3 originalForce)
    {
        Debug.DrawRay(transform.position, forceToDraw * 2, Color.green, 4);
        Debug.DrawRay(transform.position, forceToDraw, Color.blue, 4);
        Debug.DrawRay(transform.position, originalForce * 2, Color.red, 4);
        Debug.DrawRay(transform.position, originalForce, Color.yellow, 4);
        Debug.Log("collision impulse: " + collision.impulse);
        Debug.Log("last frame velocity: " + VelocityOnLastFrame);
        Debug.Log("velocity pre forces: " + GetVelocity());
    }
}