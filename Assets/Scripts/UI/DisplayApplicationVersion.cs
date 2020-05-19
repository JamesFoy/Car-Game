using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayApplicationVersion : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Space Racers v" + Application.version + ", built using Unity " + Application.unityVersion;
    }
}