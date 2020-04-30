using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Car/Car Data")]
public class CarData : ScriptableObject
{
    [Header("Dynamic Values - these change at runtime, don't edit")]
    // Dynamic values, changed by player behaviour
    public float health;
    public float speed;

    // Base car properties - Variables that are used for car setup
    [Header("Car Properties - set these"), Tooltip("suspension amount is how high the car is off the ground.... been setup for height of 6, so DONT TOUCH!!")]
    public float suspensionAmount;
    public float turnSpeed;
    public float maxSpeed;
}