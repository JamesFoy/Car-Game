using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathControl : MonoBehaviour
{
    Transform path; //variable for the path parent object that contains all the nodes

    [SerializeField]
    private List<Transform> nodes; //Creates a list of transforms

    Vector3 nodeTarget; //Creates a vector3

    [SerializeField]
    int currentNode = 0; //Sets the current node number (can be used to skip nodes in testing but also needed for the AI to work)

    Rigidbody rb;
    CarInfo carInfo;
    CarController carController;

    float angleTarget; //Creates a float
    float turn; //Creates a float

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        carInfo = GetComponent<CarInfo>();
        carController = GetComponent<CarController>();

        path = Path.Instance.transform; //Gets the instance of path

        //if path is not null then create a list and get all children of the path object (works in the same way as the Path script)
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

    //Method for applying a forward movement to the AI car
    private void ApplyMovement()
    {
        carInfo.carStats.speed = Mathf.LerpAngle(carInfo.carStats.speed, carInfo.carStats.maxSpeed, Time.deltaTime / 2); //Sets the cars speed to gradually increase in the same way as the player cars

        rb.AddForce(transform.forward * carInfo.carStats.speed, ForceMode.Acceleration); //Applies a force to push the AI car forward based on the car speed (works in the same way as the player car)
    }

    //Method for finding and applying the correct rotation to the car when going between nodes
    private void ApplyRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(nodes[currentNode].position - transform.position); //targetRotation is the current node - the players position

        nodeTarget = Vector3.zero; //Makes sure nodeTarget is 0

        //if currentNode is equal to the last node
        if (currentNode == nodes.Count - 1)
        {
            //then the nodeTarget to use is the first in the list (makes sure the car loops the Path)
            nodeTarget = nodes[currentNode - nodes.Count + 1].localPosition;
        }
        else
        {
            //Otherwise the nodeTarget is the node after the current node
            nodeTarget = nodes[currentNode + 1].localPosition;
        }

        angleTarget = Vector3.Angle(nodes[currentNode].localPosition, nodeTarget); //Finds the angle between the current node position and the target node (used for calcualting how much rotation the car should use)
        Debug.Log(angleTarget);

        //If statements for changing how sharp/quick the car should rotate towards the current node when passing the previous node (makes the car rotate slowly around nodes... the higher the angle the quicker the car needs to turn)
        if (angleTarget < 10f)
        {
            turn = Mathf.LerpAngle(turn, angleTarget, Time.deltaTime / 10);
        }
        else if (angleTarget > 30 && angleTarget < 50)
        {
            turn = Mathf.LerpAngle(turn, angleTarget, Time.deltaTime / 100);
        }
        else if (angleTarget > 50)
        {
            turn = Mathf.LerpAngle(turn, angleTarget, Time.deltaTime / 100);
        }

        //Applies the rotation to the AI car
        rb.MoveRotation(Quaternion.RotateTowards(transform.localRotation, targetRotation, turn));
    }


    //Method used to check the distance between the AI cars postion and the current nodes position, if the car gets within a certain distance it changes the current node to the next one in the list
    private void CheckWaypointLength()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 20f)
        {
            //Checks if the current node is the last in the list if so the current node needs to be the first in the list
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            //otherwise increase the current node number
            else
            {
                currentNode++;
            }
        }
    }
}
