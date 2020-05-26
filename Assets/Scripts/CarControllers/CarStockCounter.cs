using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStockCounter : MonoBehaviour
{
    public GameEvent carEliminated;
    bool hasThisCarBeenRecentlyReset = false;
    private void OnEnable()
    {
        ResetTriggeredObjectToLastKnownCheckpoint.announceCarReset += CheckForZeroStocks;
    }
    public void CheckForZeroStocks()
    {
        if (GetComponent<CarInfo>() != null)
        {
            if (GetComponent<CarInfo>().carStats.stocks <= 0)
            {
                ResetTriggeredObjectToLastKnownCheckpoint.announceCarReset -= CheckForZeroStocks;
                Destroy(gameObject);
                carEliminated.Raise();
            }
        }
    }
    public void ReduceThisCarStockByOne()
    {
        if (GetComponent<CarInfo>() && !hasThisCarBeenRecentlyReset)
        {
            GetComponent<CarInfo>().carStats.stocks--;
            GetComponent<CarInfo>().carStats.health = 0;
            hasThisCarBeenRecentlyReset = true;
            StartCoroutine(carResetCheckTimer());
            CheckForZeroStocks();
        }
    }
    IEnumerator carResetCheckTimer()
    {
        yield return new WaitForSeconds(1);
        hasThisCarBeenRecentlyReset = false;
        yield return null;
    }
}
