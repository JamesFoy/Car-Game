using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugFlip : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    Text rayPoint1Text, rayPoint2Text, rayPoint3Text, rayPoint4Text;

    [SerializeField]
    Transform rayPoint1, rayPoint2, rayPoint3, rayPoint4, groundCheck;

    float dis, actualDistance;

    float compressionRatio;

    public float suspensionAmount, acceleration, deceleration, speed, maxSpeed;

    public bool onGround;

    Vector3 surfaceImpactPoint, surfaceImpactNormal;

    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * acceleration, ForceMode.Acceleration);
        }

        ResetRigidBody();

        float turn = Input.GetAxis("Horizontal");

        rb.AddTorque(transform.up * 3 * turn);

        rayPoint1Text.text = "" + compressionRatio;
        rayPoint2Text.text = "" + compressionRatio;
        rayPoint3Text.text = "" + compressionRatio;
        rayPoint4Text.text = "" + compressionRatio;

        Debug.DrawRay(rayPoint1.position, rayPoint1.forward * 0.5f, Color.red);

        Debug.DrawRay(rayPoint2.position, rayPoint2.forward * 0.5f, Color.red);

        Debug.DrawRay(rayPoint3.position, rayPoint3.forward * 0.5f, Color.red);

        Debug.DrawRay(rayPoint4.position, rayPoint4.forward * 0.5f, Color.red);

        Debug.DrawRay(groundCheck.position, groundCheck.up * 1f, Color.blue);

        RaycastHit hit;

        // Does the ray intersect any objects
        if (Physics.Raycast(rayPoint1.position, rayPoint1.forward * 0.6f, out hit))
        {
            CalculateCompression(hit, rayPoint1);
        }
        if (Physics.Raycast(rayPoint2.position, rayPoint2.forward * 0.6f, out hit))
        {
            CalculateCompression(hit, rayPoint2);
        }
        if (Physics.Raycast(rayPoint3.position, rayPoint3.forward * 0.6f, out hit))
        {
            CalculateCompression(hit, rayPoint3);
        }
        if (Physics.Raycast(rayPoint4.position, rayPoint4.forward * 0.6f, out hit))
        {
            CalculateCompression(hit, rayPoint4);
        }
        if (compressionRatio == 0)
        {
            acceleration = 10;
            rb.mass = 1f;
            rb.drag = 0;
            rb.angularDrag = 0;
        }
        else
        {
            acceleration = 40;
            rb.mass = 1.28f;
            rb.drag = 4;
            rb.angularDrag = 10;
        }
    }

    private void ResetRigidBody()
    {
        rb.mass = 1;
        rb.drag = 0;
        rb.angularDrag = 0;
    }

    void CalculateCompression(RaycastHit hitPoint, Transform originPoint)
    {
        dis = Vector3.Distance(hitPoint.point, originPoint.transform.position) * 2;
        float clampedDistance = Mathf.Clamp(dis, 0, 1);
        actualDistance = (float)System.Math.Round(clampedDistance, 1);
        compressionRatio = 1.0f - actualDistance;

        Debug.Log(compressionRatio);

        rb.AddForceAtPosition(transform.up * compressionRatio * suspensionAmount, originPoint.transform.position, ForceMode.Force);
        rb.AddForceAtPosition(-transform.up * (compressionRatio - 1.0f), originPoint.transform.position, ForceMode.Force);
    }
}
