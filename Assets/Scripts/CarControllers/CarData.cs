using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Car/Car Data")]
public class CarDataSO : ScriptableObject
{
    // Dynamic values, changed by player behaviour
    public float health;
    public float speed;

    // Base car properties - make sure to instantiate a copy of the base car data SO
    public float accelerationForce;
    public float suspensionForce;
    public float turnSpeed;
    public float maxSpeed;
}