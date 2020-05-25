using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOverTime : MonoBehaviour
{
    public float timeAmount;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(Deactivate(timeAmount));
    }

    IEnumerator Deactivate(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
