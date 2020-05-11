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
        Checkpoint c = CheckpointManager.Instance.FindCheckpointThatContainsThisGameObject(other.gameObject);
        if (c != null)
        {
            if (other.GetComponent<Rigidbody>())
            {
                Rigidbody rb = other.GetComponent<Rigidbody>();
                rb.position = c.transform.position;
                rb.rotation = c.transform.rotation;
            }
            else
            {
                other.transform.position = c.transform.position;
                other.transform.rotation = c.transform.rotation;
            }
            if (other.GetComponent<CarStockCounter>())
            {
                other.GetComponent<CarStockCounter>().ReduceThisCarStockByOne();
                announceCarReset();
            }
        }
    }
}
