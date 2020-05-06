using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    List<Checkpoint> checkpoints = new List<Checkpoint>();
    public static CheckpointManager Instance;
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
    /// <summary>
    /// Adds the checkpoint instance provided to the list of checkpoints in CheckpointManager
    /// </summary>
    /// <param name="checkpoint"></param>
    public void AddThisCheckpointToCheckpointList(Checkpoint checkpoint)
    {
        if (!checkpoints.Contains(checkpoint))
        {
            checkpoints.Add(checkpoint);
        }
    }
    /// <summary>
    /// Removes the given GameObject from all checkpoints
    /// </summary>
    /// <param name="car"></param>
    public void RemoveThisCarFromAllCheckpoints(GameObject car)
    {
        foreach (Checkpoint a in checkpoints)
        {
            if (a.CurrentCars.Contains(car))
            {
                a.RemoveCarFromThisCheckpoint(car);
            }
        }
    }
}
