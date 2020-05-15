using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyCheckpoint : Checkpoint
{
    bool carPassedCheckpoint = false;
    TMP_Text checkpointText;
    float repairBonus = 20f;

    private void Start()
    {
        CheckForExistingCheckpointManager();
        checkpointText = GetComponentInChildren<TMP_Text>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarInfo>())
        {
            CheckpointTriggered(other);
            KeyCheckpointTriggered(other);
        }
    }

    private void KeyCheckpointTriggered (Collider other)
    {
        if (!carPassedCheckpoint)
        {
            Debug.Log(other.gameObject.name + " has reached a key checkpoint first: " + this.name);
            carPassedCheckpoint = true;

            if (other.GetComponent<CarInfo>().carStats.health > repairBonus)
            {
                // adjusted to prevent negative repair values
                other.GetComponent<CarInfo>().carStats.health -= repairBonus;
            }
            checkpointText.gameObject.SetActive(false);
            StartCoroutine(DelayCheckPoint(60f));
        }
    }

    IEnumerator DelayCheckPoint(float timerAmount)
    {
        yield return new WaitForSeconds(timerAmount);
        checkpointText.gameObject.SetActive(true);
        carPassedCheckpoint = false;
    }
}
