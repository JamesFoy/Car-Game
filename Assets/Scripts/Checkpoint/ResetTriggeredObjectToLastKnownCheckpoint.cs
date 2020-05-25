using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTriggeredObjectToLastKnownCheckpoint : MonoBehaviour
{
    public delegate void CarHasEnteredResetZone();
    public static event CarHasEnteredResetZone announceCarReset;
    private void Start()
    {
        if (CheckpointManager.Instance != null)
        {
        }
        else
        {
            Destroy(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        CheckpointManager.Instance.ResetThisGameObjectToItsLastCheckpoint(other.gameObject);
        {
            if (other.GetComponent<CarStockCounter>())
            {
                other.GetComponent<CarStockCounter>().ReduceThisCarStockByOne();
                announceCarReset();
            }
        }
    }
}
