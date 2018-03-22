using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    public static Settings instance;

    [SerializeField]
    AzureSkyController SkyObject;

    [SerializeField]
    AudioSource vocals_Audio;

    [SerializeField]
    AudioSource background_Audio;

    [SerializeField]
    WindZone WindObject;

    [HideInInspector]
    public string Mode = "Day";

    [HideInInspector]
    public string Audio_Track = "track1";

    [HideInInspector]
    public float track_volume = 70.0f;

    [HideInInspector]
    public float background_volume = 100.0f;

    //[HideInInspector]
    //public AudioClip[] vocals;

    [HideInInspector]
    public Dictionary<string, AudioClip> audioMap;

    public Dropdown uiDropDown;

    public Slider uibackgroundVolume;
    public Slider uivocalVolume;
    public Slider uitimeline;

    private void Awake()
    {
        instance = this;
        audioMap = new Dictionary<string, AudioClip>();
    }

    void Start()
    {

        //vocals = new AudioClip[3];

        //vocals[0] = (AudioClip)Resources.Load("Guide_1");
        //vocals[1] = (AudioClip)Resources.Load("Guide_2");
        //vocals[2] = (AudioClip)Resources.Load("Mindfulness3");
        AudioClip temp = (AudioClip)Resources.Load("Guide_1");
        audioMap.Add("Guide_1", temp);

        temp = (AudioClip)Resources.Load("Guide_2");
        audioMap.Add("Guide_2", temp);

        uiDropDown.ClearOptions();
        uiDropDown.AddOptions(Settings.instance.audioMap.Keys.ToList());

        vocals_Audio.clip = audioMap["Guide_1"];
        vocals_Audio.Play();
    }

    // Changing timeline mode from Web UI
    public void changeMode(string mode)
    {
        float value = 0.0f;
        switch (mode)
        {
            case "Day":
                Mode = "Day";
                SkyObject.Azure_Timeline = 8.00f;
                value = 8f / 24f;
                break;
            case "Noon":
                Mode = "Noon";
                SkyObject.Azure_Timeline = 13.50f;
                value = 13.5f / 24f;
                break;
            case "Evening":
                Mode = "Evening";
                SkyObject.Azure_Timeline = 17.30f;
                value = 17.3f / 24f;
                break;
            case "Night":
                Mode = "Night";
                SkyObject.Azure_Timeline = 21.00f;
                value = 21f / 24f;
                break;

        }
        uitimeline.value = value;
    }

    // Changing Timeline from In-game UI
    public void changeTimeline(float value)
    {
        float max = 24.0f;
        SkyObject.Azure_Timeline = max * value;
    }

    // Changing Guide from Web UI
    public void changeAudioTrack(string value)
    {

        switch (value)
        {
            case "track1":
                vocals_Audio.clip = audioMap["Guide_1"];
                Audio_Track = "track1";
                uiDropDown.value = 0;
                break;
            case "track2":
                vocals_Audio.clip = audioMap["Guide_2"];
                Audio_Track = "track2";
                uiDropDown.value = 1;
                break;
            default:    //default case is executed when  user plays uploaded audio
                vocals_Audio.clip = audioMap[value];
                Audio_Track = value;
                break;
        }

        vocals_Audio.Play();
    }

    // Changing Guide from In-game UI
    public void changeAudioTrack_fromUI(int value)
    {
        string selected_audio = uiDropDown.captionText.text;
        string temp = "";
        switch (selected_audio)
        {
            case "Guide_1":
                temp = "track1";
                break;
            case "Guide_2":
                temp = "track2";
                break;
            default:        //default case is executed when  user plays uploaded audio
                temp = selected_audio;
                break;
        }

        changeAudioTrack(temp);
        //vocals_Audio.clip = vocals[value];
        //vocals_Audio.Play();
    }

    // Changing Guide from Web UI & In-game UI
    public void changeTrackVolume(float value)
    {
        track_volume = value;
        vocals_Audio.volume = value;
        uivocalVolume.value = value;
    }

    // Changing Guide from Web UI & In-game UI
    public void changeBackgroundVolume(float value)
    {
        background_volume = value;
        background_Audio.volume = value;
        uibackgroundVolume.value = value;
    }




    // Changing Cloud speed from In-game UI
    public void cloudSpeed(float value)
    {
        float max = 0.5f;
        SkyObject.Azure_DynamicCloudSpeed = max * value;
        //Debug.Log(SkyObject.Azure_DynamicCloudSpeed);
    }

    // Changing Wind speed from In-game UI
    public void windSpeed(float value)
    {
        float max = 15.0f;
        WindObject.windMain = max * value;
        //Debug.Log(WindObject.windMain);
    }


}
