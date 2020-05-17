using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInfo : MonoBehaviour
{
    [SerializeField] CarStats baseCarStats;
    public CarStats carStats;

    private void Start()
    {
        CheckForValidCarStats();
    }
    private void Update()
    {
        CheckForValidCarStats();
    }
    public void CheckForValidCarStats()
    {
        if (carStats == null)
        {
            carStats = Instantiate(baseCarStats);
            carStats.name = gameObject.name + " Car Stats";
        }
    }
}