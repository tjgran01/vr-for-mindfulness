using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ChangeViews : MonoBehaviour
{

    public Transform SpawnPoint_1;
    public GameObject VRTK_Camera;


    void Start()
    {

        GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += new ControllerInteractionEventHandler(ThirdPersonView);

        //spawn1.position = new Vector3(0.0f, 3.3f, 1.4f);
        
       
    }

    private void ThirdPersonView(object sender, ControllerInteractionEventArgs e)
    {
        VRTK_Camera.transform.position = SpawnPoint_1.position;
        VRTK_Camera.transform.rotation = SpawnPoint_1.rotation;
    }
}
