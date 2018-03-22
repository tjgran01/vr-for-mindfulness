using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using SimpleJSON;
using System;

public class CaptureData : MonoBehaviour
{
    public int gap = 1;

    private IEnumerator coroutine;
    WebClient client;
    string url = "http://localhost:3000/captureData";

    private void Start()
    {
        client = new WebClient();

        coroutine = sampleCoroutine();
        StartCoroutine(coroutine);
    }


    IEnumerator sampleCoroutine()
    {
        while (true)
        {
            getRequest();

            yield return new WaitForSeconds(gap);
        }

    }


    private void getRequest()
    {
        try
        {

            string jsonResponse = client.DownloadString(url);

            var dataFromServer = JSON.Parse(jsonResponse);

            //Changing the timeline (Day,Noon,Evening,Night)
            string mode = dataFromServer["mode"];
            if (mode != Settings.instance.Mode)
            {
                Settings.instance.changeMode(mode);
            }

            //Changing the audio track (track1,track2,track3)
            string audio = dataFromServer["audioTrack"];
            if (audio != Settings.instance.Audio_Track)
            {
                Settings.instance.changeAudioTrack(audio);
            }

            //Changing the audio track volume (0 <= value <= 100)
            string audio_volume_temp = dataFromServer["audioVolume"];
            float audio_volume = (float)Convert.ToDouble(audio_volume_temp);
            if (audio_volume != Settings.instance.track_volume)
            {
                Settings.instance.changeTrackVolume(audio_volume / 100.0f);
            }

            //Changing the background track volume (0 <= value <= 100)
            string background_volume_temp = dataFromServer["backgroundVolume"];
            float background_volume = (float)Convert.ToDouble(background_volume_temp);
            if (background_volume != Settings.instance.background_volume)
            {
                Settings.instance.changeBackgroundVolume(background_volume / 100.0f);
            }

            //Debug.Log(x["mode"]);
            //Console.WriteLine(jsonResponse);
        }
        catch (WebException ex)
        {
            //Debug.Log("Exception Occured");
            //Console.WriteLine("Exception occured");
        }
    }

    //void Update()
    //{

    //    try
    //    {
    //        WebClient client = new WebClient();
    //        string url = "http://localhost:3000/captureData";
    //        string jsonResponse = client.DownloadString(url);

    //        var dataFromServer = JSON.Parse(jsonResponse);

    //        //Changing the timeline (Day,Noon,Evening,Night)
    //        string mode = dataFromServer["mode"];
    //        if (mode != Settings.instance.Mode)
    //        {
    //            Settings.instance.changeMode(mode);
    //        }

    //        //Changing the audio track (track1,track2,track3)
    //        string audio = dataFromServer["audioTrack"];
    //        if (audio != Settings.instance.Audio_Track)
    //        {
    //            Settings.instance.changeAudioTrack(audio);
    //        }

    //        //Changing the audio track volume (0 <= value <= 100)
    //        string audio_volume_temp = dataFromServer["audioVolume"];
    //        float audio_volume = (float)Convert.ToDouble(audio_volume_temp);
    //        if (audio_volume != Settings.instance.track_volume)
    //        {
    //            Settings.instance.changeTrackVolume(audio_volume / 100.0f);
    //        }

    //        //Changing the background track volume (0 <= value <= 100)
    //        string background_volume_temp = dataFromServer["backgroundVolume"];
    //        float background_volume = (float)Convert.ToDouble(background_volume_temp);
    //        if (background_volume != Settings.instance.background_volume)
    //        {
    //            Settings.instance.changeBackgroundVolume(background_volume/100.0f);
    //        }

    //        //Debug.Log(x["mode"]);
    //        //Console.WriteLine(jsonResponse);
    //    }
    //    catch (WebException ex)
    //    {
    //        //Debug.Log("Exception Occured");
    //        //Console.WriteLine("Exception occured");
    //    }
    //}
}