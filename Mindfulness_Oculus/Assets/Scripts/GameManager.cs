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
    GameObject background_Audio;

    [SerializeField]
    GameObject vocal_Audio;

    private string[] scenes = { "DemoSceneFree", "OculusAvatar", "Demo" };

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(background_Audio);
        DontDestroyOnLoad(vocal_Audio);

    }

    // Use this for initialization
    void Start () {
		
	}

    public void changeScene()
    {
        System.Random rnd = new System.Random();
        var index = rnd.Next(0, scenes.Length);

        SceneManager.LoadScene(scenes[index]);
    }
}
