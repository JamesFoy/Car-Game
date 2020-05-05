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
    void CheckForValidCarStats()
    {
        if (carStats == null)
        {
            carStats = Instantiate(baseCarStats);
            carStats.name = "Current Car Stats";
        }
    }
}
