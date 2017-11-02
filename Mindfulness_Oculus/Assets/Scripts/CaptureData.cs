using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using SimpleJSON;

public class CaptureData : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        try
        {
            WebClient client = new WebClient();
            string url = "http://localhost:3000/captureData";
            string jsonResponse = client.DownloadString(url);

            var x = JSON.Parse(jsonResponse);

            Debug.Log(x["mode"]);
            //Console.WriteLine(jsonResponse);
        }
        catch (WebException ex)
        {
            Debug.Log("Exception Occured");
            //Console.WriteLine("Exception occured");
        }
    }
}
