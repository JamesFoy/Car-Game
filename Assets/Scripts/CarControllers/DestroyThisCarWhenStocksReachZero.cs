using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisCarWhenStocksReachZero : MonoBehaviour
{
    private void OnEnable()
    {
        ReduceStockByOneWhenEnteringThisTrigger.stockReduced += CheckForZeroStocks;
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
}
