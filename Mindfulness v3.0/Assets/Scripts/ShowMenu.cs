using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ShowMenu : MonoBehaviour {

    bool menuStatus = false;

    [SerializeField]
    GameObject Menu;

    [SerializeField]
    GameObject GameManager;
	
	void Start () {

        GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += new ControllerInteractionEventHandler(DisplayMenu);

        Menu.SetActive(false);
        GameManager.GetComponent<CaptureData>().enabled = true;
        //if(Menu == null)
        //{
        //    Menu = GameObject.Find("Menu_Canvas");
        //    menuStatus = Menu.activeInHierarchy;
        //}

    }

    private void DisplayMenu(object sender, ControllerInteractionEventArgs e)
    {
        //Debug.Log("B pressed");

        //Disable menu + enable capture data
        if (menuStatus)
        {
            menuStatus = false;
            GameManager.GetComponent<CaptureData>().enabled = true;
        }
        else //Enable Menu + disable capture data
        {
            menuStatus = true;
            GameManager.GetComponent<CaptureData>().enabled = false;
        }

        Menu.SetActive(menuStatus);
    }
}
