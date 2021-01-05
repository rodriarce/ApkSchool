using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScene : MonoBehaviour
{

    public static AudioScene audioScene;
    public AudioSource audioGame;
    public AudioClip clipScene;
    public AudioSource audioVictory;
    public AudioClip clipVictory;
    private bool isPlayMusic = true;



    private void Awake()
    {
        if (audioScene == null)
        {
            audioScene = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audioGame.clip = clipScene;
        audioGame.Play();
        
    }
    public void PlayAudioLvl()
    {
        if (isPlayMusic)
        {
            isPlayMusic = false;
            audioGame.Stop();
        }
        else
        {
            isPlayMusic = true;
            audioGame.Play();
        }
    }
    public void PlayAudioVictory()
    {
        audioVictory.clip = clipVictory;
        audioVictory.Play();
    }
    public void PlayAudioCoin()
    {
        //audio
    }
    




    // Update is called once per frame
    void Update()
    {
        
    }
}
