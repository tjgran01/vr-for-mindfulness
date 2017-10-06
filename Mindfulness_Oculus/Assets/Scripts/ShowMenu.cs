using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ShowMenu : MonoBehaviour {

    bool menuStatus = true;

    GameObject Menu;

	
	void Start () {

        GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += new ControllerInteractionEventHandler(DisplayMenu);

        Menu = GameObject.Find("Menu_Canvas");
        menuStatus = Menu.activeInHierarchy;
    }

    private void DisplayMenu(object sender, ControllerInteractionEventArgs e)
    {
        //Debug.Log("B pressed");
        if (menuStatus)
        {
            menuStatus = false;
        }
        else
        {
            menuStatus = true;
        }

        Menu.SetActive(menuStatus);
    }
}
