using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ChangeViews : MonoBehaviour
{

    //public Transform SpawnPoint_1;

    public GameObject VRTK_Camera;
    public GameObject Character;

    public Transform[] spawnPoints;

    private int spawnIndex = 0;

    void Start()
    {

        VRTK_Camera.transform.position = spawnPoints[spawnIndex].position;
        VRTK_Camera.transform.rotation = spawnPoints[spawnIndex].rotation;
        Character.SetActive(false);


        GetComponent<VRTK_ControllerEvents>().ButtonOnePressed += new ControllerInteractionEventHandler(ThirdPersonView);
        //spawn1.position = new Vector3(0.0f, 3.3f, 1.4f);
        
       
    }

    private void ThirdPersonView(object sender, ControllerInteractionEventArgs e)
    {
        spawnIndex++;
        spawnIndex = spawnIndex % spawnPoints.Length;

        VRTK_Camera.transform.position = spawnPoints[spawnIndex].position;
        VRTK_Camera.transform.rotation = spawnPoints[spawnIndex].rotation;

        if(spawnIndex == 0)
        {
            Character.SetActive(false);
        }else
        {
            Character.SetActive(true);
        }
    }
}
