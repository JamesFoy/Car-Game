using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public List<GameObject> CurrentCars { get; } = new List<GameObject>();
    public int lapProgress;

    private void Start()
    {
        CheckForExistingCheckpointManager();
    }
    protected void CheckForExistingCheckpointManager()
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
        if (other.GetComponent<CarInfo>())
        {
            CheckpointTriggered(other);
        }
    }
    protected void CheckpointTriggered(Collider other)
    {
        if (!CurrentCars.Contains(other.gameObject))
        {
            CheckpointManager.Instance.RemoveThisGameObjectFromAllCheckpoints(other.gameObject);
            AddGameObjectToThisCheckpoint(other.gameObject);
            if (other.GetComponent<CarInfo>())
            {
                other.GetComponent<CarInfo>().carStats.lapProgress = lapProgress;
            }
        }
    }
    protected void AddGameObjectToThisCheckpoint(GameObject a)
    {
        CurrentCars.Add(a);
        Debug.Log(a.name + " added to " + gameObject.name);
    }
    /// <summary>
    /// Removes provided GameObject from this checkpoint
    /// </summary>
    /// <param name="a"></param>
    public void RemoveGameObjectFromThisCheckpoint(GameObject a)
    {
        CurrentCars.Remove(a);
        Debug.Log(a.name + " removed from checkpoint " + gameObject.name);
    }
}
