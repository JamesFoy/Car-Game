using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Car/Car Data")]
public class CarDataSO : ScriptableObject
{
    public FloatReference health;
    public FloatReference accelerationForce;
}
