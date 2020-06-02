using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformThisObjectPerFrame : MonoBehaviour
{
    [Range(-10f, 10f)] public float xAxis;
    [Range(-10f, 10f)] public float yAxis;
    [Range(-10f, 10f)] public float zAxis;
    private void Update()
    {
        transform.position += new Vector3(xAxis * Time.deltaTime, yAxis * Time.deltaTime, zAxis * Time.deltaTime);
    }
}
