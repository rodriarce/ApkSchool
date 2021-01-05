using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioMenu;
    public AudioClip clipMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        audioMenu.clip = clipMenu;
        audioMenu.Play();
    }

    public void DisableAudio()
    {
        if (!audioMenu.mute)
        {
           audioMenu.mute = true;
        }
        else
        {
            audioMenu.mute = false;
        }

        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
