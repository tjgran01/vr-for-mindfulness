using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetting : MonoBehaviour {

    AudioSource background;

	// Use this for initialization
	void Start () {
        background = GetComponent<AudioSource>();
	}
	
	public void BackgroundVolume(float value)
    {
        background.volume = value;
    }
}
