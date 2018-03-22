using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mouse_EnvironmentUI : MonoBehaviour {

    [SerializeField]
    Button dayButton;

    [SerializeField]
    Button nightButton;

    [SerializeField]
    Button sceneButton;

    GraphicRaycaster gr;

    void Start()
    {
        gr = this.GetComponent<GraphicRaycaster>();
    }

    // Update is called once per frame
    void Update () {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {

            Debug.Log("Clicked");
            PointerEventData ped = new PointerEventData(null);
            //Set required parameters, in this case, mouse position
            ped.position = Input.mousePosition;
            //Create list to receive all results
            List<RaycastResult> results = new List<RaycastResult>();
            //Raycast it
            gr.Raycast(ped, results);

            Debug.Log(results.Count);

            if(results.Count > 0)
            {
                Debug.Log(results[results.Count - 1].gameObject.name);
                string uiElement = results[results.Count - 1].gameObject.name;

                uiMouseInteraction(uiElement);
            }
            results.Clear();
        }

    }

    void uiMouseInteraction(string uiElement)
    {
        switch (uiElement)
        {
            case "Day_Button":
                dayButton.onClick.Invoke();
                break;

            case "Night_Button":
                nightButton.onClick.Invoke();
                break;

            case "sceneChange":
                sceneButton.onClick.Invoke();
                break;

            default:
                Debug.Log("Default test case executed");
                break;
        }
    }
}
