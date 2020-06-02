using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AdjustDOFAtRuntime : MonoBehaviour
{
    PostProcessProfile postProfile;
    DepthOfField DOF;
    private void OnEnable()
    {
        postProfile = GetComponent<PostProcessVolume>().profile;
        postProfile.TryGetSettings(out DOF);
    }
    private void Update()
    {
        DOF.enabled.value = true;
        DOF.focusDistance.value += 1.6f * Time.deltaTime;
    }
}
