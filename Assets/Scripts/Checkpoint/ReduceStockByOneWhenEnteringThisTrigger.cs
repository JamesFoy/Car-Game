using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceStockByOneWhenEnteringThisTrigger : MonoBehaviour
{
    public delegate void StockReducedByOne();
    public static event StockReducedByOne stockReduced;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarInfo>())
        {
            other.GetComponent<CarInfo>().carStats.stocks--;
            stockReduced();
        }
    }
}
