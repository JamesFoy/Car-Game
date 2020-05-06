using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public List<GameObject> CurrentCars { get; } = new List<GameObject>();

    private void Start()
    {
        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.AddThisCheckpointToCheckpointList(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        CheckpointManager.Instance.RemoveThisCarFromAllCheckpoints(other.gameObject);
        AddCarToThisCheckpoint(other.gameObject);
    }
    void AddCarToThisCheckpoint(GameObject car)
    {
        CurrentCars.Add(car);
        Debug.Log(car.name + " added to " + gameObject.name);
    }
    /// <summary>
    /// Removes provided GameObject from this checkpoint
    /// </summary>
    /// <param name="car"></param>
    public void RemoveCarFromThisCheckpoint(GameObject car)
    {
        CurrentCars.Remove(car);
        Debug.Log(car.name + " removed from checkpoint " + gameObject.name);
    }
}
