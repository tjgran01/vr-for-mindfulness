using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathManager : MonoBehaviour {

    public ParticleSystem[] CharacterBreath = new ParticleSystem[2];
    public ParticleSystem[] CameraBreath = new ParticleSystem[2];

    public static BreathManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //player is either camera or character
    public void Inhale(string player)
    {
        if(player == "character")
        {
            CharacterBreath[0].Play();

        }else if(player == "camera")
        {
            CameraBreath[0].Play();
        }
    }

    //player is either camera or character
    public void Exhale(string player)
    {
        if (player == "character")
        {
            CharacterBreath[1].Play();

        }
        else if (player == "camera")
        {
            CameraBreath[1].Play();
        }
    }

    public void StopBreathing()
    {
        CharacterBreath[1].Stop();
        CharacterBreath[0].Stop();

        CameraBreath[0].Stop();
        CameraBreath[1].Stop();
    }
}
