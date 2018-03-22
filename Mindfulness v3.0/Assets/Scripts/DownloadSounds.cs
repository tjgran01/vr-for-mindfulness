using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class DownloadSounds : MonoBehaviour {

    string absolutePath = "./AudioFiles"; // relative path to where the app is running

    //compatible file extensions
    string[] fileTypes = { "ogg", "wav" };

    FileInfo[] files;

    void Start()
    {
        //being able to test in unity
        if (Application.isEditor) absolutePath = "Assets/AudioFiles/";
        
        reloadSounds();
    }

    void reloadSounds()
    {
        DirectoryInfo info = new DirectoryInfo(absolutePath);
        files = info.GetFiles();

        //check if the file is valid and load it
        foreach (FileInfo f in files)
        {
            if (validFileType(f.FullName))
            {
                //Debug.Log("Start loading "+f.FullName);
                StartCoroutine(loadFile(f.FullName));
            }
        }
    }



    bool validFileType(string filename)
    {
        foreach (string ext in fileTypes)
        {
            if (filename.IndexOf(ext) > -1) return true;
        }
        return false;
    }

    IEnumerator loadFile(string path)
    {
        WWW www = new WWW("file://" + path);

        AudioClip myAudioClip = www.GetAudioClip();
        //while (!myAudioClip.isReadyToPlay)
        while (myAudioClip.loadState != AudioDataLoadState.Loaded)
            yield return www;

        AudioClip clip = www.GetAudioClip(false);
        string[] parts = path.Split('\\');
        clip.name = parts[parts.Length - 1];

        Settings.instance.audioMap.Add(clip.name, clip);


        reloadDropDown();
        //clipNames = new List<string>(audioMap.Keys);
        ////Debug.Log(clipNames);

        //Dropdown m_down = dropDownMenu.GetComponent<Dropdown>();
        //m_down.ClearOptions();
        //m_down.AddOptions(clipNames);
        ////clips.Add(clip);

        ////src.clip = clips[0];
        ////src.Play();
    }

    void reloadDropDown()
    {
        Settings.instance.uiDropDown.ClearOptions();
        Settings.instance.uiDropDown.AddOptions(Settings.instance.audioMap.Keys.ToList());
    }
}
