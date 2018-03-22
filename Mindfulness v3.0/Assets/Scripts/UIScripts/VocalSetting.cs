using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VocalSetting : MonoBehaviour {

    AudioSource vocal;

    // Use this for initialization
    void Start()
    {
        vocal = GetComponent<AudioSource>();
    }

    public void VocalVolume(float value)
    {
        vocal.volume = value;
    }
}
