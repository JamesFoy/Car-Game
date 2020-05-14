using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathControl : MonoBehaviour
{
    [SerializeField]
    Transform path;

    [SerializeField]
    private List<Transform> nodes;

    Vector3 nodeTarget;

    [SerializeField]
    int currentNode = 0;

    Rigidbody rb;
    CarInfo carInfo;
    CarController carController;

    bool isFalling;
    float angleTarget;
    float turn;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        carInfo = GetComponent<CarInfo>();
        carController = GetComponent<CarController>();

        path = Path.Instance.transform;

        if (path != null)
        {
            Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
            nodes = new List<Transform>();

            for (int i = 0; i < pathTransforms.Length; i++)
            {
                if (pathTransforms[i] != path.transform)
                {
                    nodes.Add(pathTransforms[i]);
                }
            }
        }
        else
        {
            return;
        }
    } 

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyMovement();
        CheckWaypointLength();
        ApplyRotation();
    }


    private void ApplyMovement()
    {
        carInfo.carStats.speed = Mathf.LerpAngle(carInfo.carStats.speed, carInfo.carStats.maxSpeed, Time.deltaTime / 2);

        rb.AddForce(transform.forward * carInfo.carStats.speed, ForceMode.Acceleration);
    }

    private void ApplyRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(nodes[currentNode].position - transform.position);

        nodeTarget = Vector3.zero;

        if (currentNode == nodes.Count - 1)
        {
            nodeTarget = nodes[currentNode - nodes.Count + 1].localPosition;
        }
        else
        {
            nodeTarget = nodes[currentNode + 1].localPosition;
        }

        angleTarget = Vector3.Angle(nodes[currentNode].localPosition, nodeTarget);
        Debug.Log(angleTarget);

        if (angleTarget < 10f)
        {
            turn = Mathf.LerpAngle(turn, angleTarget, Time.deltaTime / 1000000);
        }
        else if (angleTarget > 30 && angleTarget < 50)
        {
            turn = Mathf.LerpAngle(turn, angleTarget, Time.deltaTime / 1000);
        }
        else if (angleTarget > 50)
        {
            turn = Mathf.LerpAngle(turn, angleTarget, Time.deltaTime / 1000);
        }

        rb.MoveRotation(Quaternion.RotateTowards(transform.localRotation, targetRotation, turn));
    }

    private void CheckWaypointLength()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 20f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }
}
