using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class LoadAudio : MonoBehaviour
{
    string path;
    public AudioSource sound;
    public WWW www;

    void Start()
    {

        sound = GetComponent<AudioSource>();
    }

    public void OpenExplorer()
    {
     //   path = EditorUtility.OpenFilePanel("Overwrite song", "", "mp3");
        GetSound();

    }

    void GetSound()
    {
        if (path != null)
        {
            UpdateSound();
        }
    }


    void UpdateSound()
    {
        WWW www = new WWW("file://"+ path);
        sound.clip = www.GetAudioClip();
        
        sound.Play();

    }

}
