using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStockCounter : MonoBehaviour
{
    bool hasThisCarBeenRecentlyReset = false;
    private void OnEnable()
    {
        ResetTriggeredObjectToLastKnownCheckpoint.announceCarReset += CheckForZeroStocks;
    }
    void CheckForZeroStocks()
    {
        if (GetComponent<CarInfo>())
        {
            if (GetComponent<CarInfo>().carStats.stocks <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void ReduceThisCarStockByOne()
    {
        if (GetComponent<CarInfo>() && !hasThisCarBeenRecentlyReset)
        {
            GetComponent<CarInfo>().carStats.stocks--;
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
