using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [SerializeField]
    GameObject canvas;

    private string[] scenes = { "DemoSceneFree", "OculusAvatar", "Demo" };

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(transform.gameObject);
        DontDestroyOnLoad(canvas);
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
