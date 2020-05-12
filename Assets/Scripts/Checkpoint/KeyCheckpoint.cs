using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyCheckpoint : MonoBehaviour
{
    bool carPassedCheckpoint = false;
    TMP_Text checkpointText;

    private void Start()
    {
        checkpointText = GetComponentInChildren<TMP_Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !carPassedCheckpoint)
        {
            Debug.Log(other.gameObject.name + " has passed reached a key checkpoint " + this.name);

            carPassedCheckpoint = true;

            if (other.gameObject.GetComponent<CarInfo>().carStats.health > 0)
            {
                other.gameObject.GetComponent<CarInfo>().carStats.health -= 20;
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
