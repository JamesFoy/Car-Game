using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script used to contain all the information about the nodes and draw them in the Unity Editior
public class Path : MonoBehaviour
{
    //Creates an instace of the path script (will need changing int the future if we plan to have muliple paths)
    public static Path Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public Color lineColor; //Varibale for the colour of the line in the editor

    public List<Transform> nodes = new List<Transform>(); //List of transforms

    //Method that is used to draw the path within the editor whenever the Path object is selected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = lineColor; //Sets the colour to the varibale colour

        Transform[] pathTransforms = GetComponentsInChildren<Transform>(); //Creates a new array of Transforms which are the children of this gameobject
        nodes = new List<Transform>(); //Makes sure the list is empty

        //Loops through all the transforms found and exludes the transform on the parent object
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != transform)
            {
                nodes.Add(pathTransforms[i]); //Adds every child object to the list
            }
        }

        //loop through the list of nodes and find the previous and current node to draw lines
        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 currentNode = nodes[i].position; //Creates a Vector3 which stores the current index of nodes
            Vector3 previousNode = Vector3.zero; //Creates a Vector3 and makes sure it is set to 0 to begin with

            //If the index is greater then one then the previousNode is the current index - 1
            if (i > 0)
            {
                previousNode = nodes[i - 1].position;
            }
            //Else if the current node is 0 or greater then the node list the previous node is the last one in the list (this makes the draw gizmo line loop)
            else if (i == 0 && nodes.Count > 1)
            {
                previousNode = nodes[nodes.Count - 1].position;
            }

            Gizmos.DrawLine(previousNode, currentNode); //Draws a line
            Gizmos.DrawWireSphere(currentNode, .5f); //Draws a sphere on each node
        }
    }
}
