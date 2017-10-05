using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkySetting : MonoBehaviour {

    [SerializeField]
    Material daySkybox;

    [SerializeField]
    Material nightSkybox;

    bool isDay = true;

	// Use this for initialization
	void Start () {
        Debug.Log(RenderSettings.skybox);
	}

    public void changeToDay()
    {
        RenderSettings.skybox = daySkybox;
        isDay = true;
    }

    public void changeToNight()
    {
        RenderSettings.skybox = nightSkybox;
        isDay = false;
    }

}
