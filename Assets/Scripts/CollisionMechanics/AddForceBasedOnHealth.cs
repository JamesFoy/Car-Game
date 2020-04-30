using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceBasedOnHealth : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] CarData car;
    Vector3 velocityOnLastFrame;
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
    void FixedUpdate()
    {
        if (frames < 1)
        {
            velocityOnLastFrame = GetVelocity();
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
            Vector3 healthScaledForce = originalForce * car.health;
            if (velocityOnLastFrame.sqrMagnitude >= GetVelocity().sqrMagnitude)
            {
                rb.AddForce(healthScaledForce);
            }
            else
            {
                rb.AddForce(-healthScaledForce);
            }
            DebugLogForces(collision, originalForce);
        }
    }
    Vector3 GetVelocity()
    {
        return rb.velocity;
    }
    void DebugLogForces(Collision collision, Vector3 originalForce)
    {
        if (velocityOnLastFrame.sqrMagnitude >= GetVelocity().sqrMagnitude)
        {
            Debug.DrawRay(transform.position, collision.impulse * 2, Color.green, 4);
            Debug.DrawRay(transform.position, collision.impulse, Color.blue, 4);
        }
        else
        {
            Debug.DrawRay(transform.position, -collision.impulse * 2, Color.green, 4);
            Debug.DrawRay(transform.position, -collision.impulse, Color.blue, 4);
        }
        Debug.Log(collision.impulse);
        Debug.Log(velocityOnLastFrame);
        Debug.Log(GetVelocity());
        Debug.Log(originalForce);
    }
}