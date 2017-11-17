using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class BreathOnClick : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        GetComponent<VRTK_ControllerEvents>().ButtonOnePressed += new ControllerInteractionEventHandler(InhaleNow);
        GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += new ControllerInteractionEventHandler(ExhaleNow);

    }

    private void InhaleNow(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("X clicked");
    }

    private void ExhaleNow(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("Y clicked");
    }
}
