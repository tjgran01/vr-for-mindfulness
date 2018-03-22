using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Oculus;

public class ChangeViews : MonoBehaviour {

    public static ChangeViews instance;

    public GameObject VRTK_Camera;
    public GameObject Character;

    public Transform[] spawnPoints;

    public GameObject localAvatar;

    public int spawnIndex = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start () {

        VRTK_Camera.transform.position = spawnPoints[spawnIndex].position;
        VRTK_Camera.transform.rotation = spawnPoints[spawnIndex].rotation;
        Character.SetActive(false);

        GetComponent<VRTK_ControllerEvents>().ButtonOnePressed += new ControllerInteractionEventHandler(ThirdPersonView);   //Clicking the A button
    }

    private void ThirdPersonView(object sender, ControllerInteractionEventArgs e)
    {
        int prev_index = spawnIndex;
        spawnIndex++;
        spawnIndex = spawnIndex % spawnPoints.Length;

        VRTK_Camera.transform.position = spawnPoints[spawnIndex].position;
        VRTK_Camera.transform.rotation = spawnPoints[spawnIndex].rotation;

        if (spawnIndex == 0) //First person view
        {
            Character.SetActive(false);
            BreathManager.instance.ChangedView("FPV");      //FPV = First person view
            localAvatar.GetComponent<OvrAvatar>().ShowFirstPerson = true;
        }
        else
        {
            if(prev_index == 0)
            {
                Character.SetActive(true);
                BreathManager.instance.ChangedView("TPV");      //TPV = Third person view
                localAvatar.GetComponent<OvrAvatar>().ShowFirstPerson = false;
            }
            
        }
    }
}
