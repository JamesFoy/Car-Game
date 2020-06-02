using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimescaleSetter : MonoBehaviour
{
    [Range(0f, 1f)] public float timeScale;
    private void Update()
    {
        Time.timeScale = timeScale;
    }
}
