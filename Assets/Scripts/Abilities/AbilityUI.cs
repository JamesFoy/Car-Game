using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public void AssignSprite(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }
    public void PlaySoundEffect(AudioClip sound)
    {
        GetComponent<AudioSource>().clip = sound;
        GetComponent<AudioSource>().Play();
    }
}
