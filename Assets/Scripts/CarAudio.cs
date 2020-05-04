using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{
    public AudioSource carSound;
    CarController carController;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        carSound.pitch = Mathf.Lerp(carSound.pitch, carController.carData.speed, 0.015f * Time.deltaTime);

        if (carController.carData.speed < 151)
        {
            carSound.pitch -= 0.009f;
        }

        carSound.pitch = Mathf.Clamp(carSound.pitch, 0.8f, 1.8f);

    }
}
