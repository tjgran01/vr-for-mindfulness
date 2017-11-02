using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

    public static Settings instance;

    [HideInInspector]
    public string Mode = "Night";

    [SerializeField]
    Material daySkybox;

    [SerializeField]
    Material nightSkybox;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeSky(string mode)
    {
        Debug.Log(mode);
        switch (mode)
        {
            case "Day":
                Mode = "Day";
                RenderSettings.skybox = daySkybox;
                break;

            case "Night":
                Mode = "Night";
                RenderSettings.skybox = nightSkybox;
                break;

        }
    }
}
