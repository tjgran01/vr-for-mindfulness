using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [SerializeField]
    GameObject canvas;

    [SerializeField]
    AudioSource background_Audio;

    [SerializeField]
    AudioSource vocals_Audio;

    [SerializeField]
    AzureSkyController SkyObject;

    [SerializeField]
    WindZone WindObject;

    [HideInInspector]
    public AudioClip[] vocals;


    private string[] scenes = { "OculusAvatar", "Demo" };

    private void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(transform.gameObject);
        //DontDestroyOnLoad(canvas);
        //DontDestroyOnLoad(background_Audio);
        //DontDestroyOnLoad(vocal_Audio);

    }

    // Use this for initialization
    void Start () {

        vocals = new AudioClip[3];

        vocals[0] = (AudioClip)Resources.Load("Mindfulness1");
        vocals[1]= (AudioClip)Resources.Load("Mindfulness2");
        vocals[2] = (AudioClip)Resources.Load("Mindfulness3");

        Debug.Log(vocals);

        vocals_Audio.clip = vocals[0];
        vocals_Audio.Play();
    }

   

    public void changeTimeline(float value)
    {
        float max = 24.0f;
        SkyObject.Azure_Timeline = max * value;
    }

    public void cloudSpeed(float value)
    {
        float max = 0.5f;
        SkyObject.Azure_DynamicCloudSpeed = max * value;
        //Debug.Log(SkyObject.Azure_DynamicCloudSpeed);
    }

    public void windSpeed(float value)
    {
        float max = 15.0f;
        WindObject.windMain = max * value;
        //Debug.Log(WindObject.windMain);
    }

    public void BackgroundVolume(float value)
    {
        background_Audio.volume = value;
    }

    public void VocalVolume(float value)
    {
        vocals_Audio.volume = value;
    }

    public void ChangeGuide(int value)
    {
        vocals_Audio.clip = vocals[value];
        vocals_Audio.Play();
    }

    //public void changeScene()
    //{
    //    System.Random rnd = new System.Random();
    //    var index = rnd.Next(0, scenes.Length);

    //    SceneManager.LoadScene(scenes[index]);
    //}

    
}
