using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DebugFlip : MonoBehaviour
{
    public UnityEvent triggerCamShake;

    Rigidbody rb;

    [SerializeField]
    Text rayPoint1Text, rayPoint2Text, rayPoint3Text, rayPoint4Text;

    [SerializeField]
    Transform rayPoint1, rayPoint2, rayPoint3, rayPoint4, groundCheck;

    float dis, actualDistance;

    float compressionRatio;

    public float suspensionAmount, turnSpeed, speed, maxSpeed;

    public Vector3 surfaceImpactNormal;

    bool onLand;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float forward = Input.GetAxis("Vertical");

        speed = Mathf.Lerp(speed, maxSpeed, forward * Time.deltaTime / 1f);

        if (forward > 0)
        {
            rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
        }

        if (forward == 0)
        {
            if (speed > 0)
            {
                speed -= 0.5f;
            }
        }

        if (forward < 0)
        {
            rb.AddForce(-transform.forward * 10, ForceMode.Acceleration);
        }

        float turn = Input.GetAxis("Horizontal");

        rb.AddTorque(transform.up * turnSpeed * turn);

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
            onLand = false;
            speed = 10;
            rb.mass = 4f;
            rb.drag = 1;
            rb.angularDrag = 5;
        }
        else
        {
            if (!onLand)
            {
                onLand = true;
                triggerCamShake.Invoke();
            }

            rb.mass = 1.28f;
            rb.drag = 4;
            rb.angularDrag = 10;
        }
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
