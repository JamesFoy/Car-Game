using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class SimpleCameraShake : MonoBehaviour
{
    public float shakeDuration = 0.3f;          //Time the Camera Shake effect will last
    public float shakeAmplitude = 1.2f;         //Cinemachine Noise Profile Parameter
    public float shakeFrequency = 2.0f;         //Cinemachine Noise Profile Parameter

    private float shakeElapsedTime = 0f;

    //Cinemachine Shake
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    public UnityEvent triggerCamShake;

    // Start is called before the first frame update
    void Start()
    {
        //Get  Virtual Camera Noise Profile
        if (virtualCamera != null)
        {
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Must Replace with trigger
        if (Input.GetKey(KeyCode.K))
        {
            triggerCamShake.Invoke();
        }

        //If the Cinemachine component is not set, avoid update
        if (virtualCamera != null || virtualCameraNoise != null)
        {
            //if Camera Shake effect is still playing
            if (shakeElapsedTime > 0)
            {
                //Set Cinemachine Camera Noise Parameters
                virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = shakeFrequency;

                //Update Shaker Timer
                shakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                //If Camera Shake effect is over, reset varibales
                virtualCameraNoise.m_AmplitudeGain = 0f;
                shakeElapsedTime = 0f;
            }
        }
    }

    public void ShakeCamera()
    {
        shakeElapsedTime = shakeDuration;
    }
}
