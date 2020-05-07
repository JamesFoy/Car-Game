using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPooler : ObjectPooler<RocketBehaviour>
{
    private void Start()
    {
        GameObject a = GetObjectFromPool().gameObject;

        GameObject b = GetObjectFromPool().gameObject;
        ReturnObjectToPool(b.GetComponent<RocketBehaviour>());
    }
}
