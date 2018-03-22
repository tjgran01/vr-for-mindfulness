using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Click2Breath : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        GetComponent<VRTK_ControllerEvents>().ButtonOnePressed += new ControllerInteractionEventHandler(BreatheNow);   //Clicking the X button on Left controller
    }

    private void BreatheNow(object sender, ControllerInteractionEventArgs e)    
    {
        BreathManager.instance.BreatheOnClick();
    }
}
