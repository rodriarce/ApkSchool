using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CovidManager : MonoBehaviour
{
    public GameObject firstInfo;
    public GameObject secondInfo;
    public GameObject thirdInfo;
    public GameObject fourthInfo;
    public GameObject fifthInfo;
    public GameObject sixtInfo;
    public TextMeshProUGUI title;
    public TextMeshProUGUI titleInfo;
    public GameObject covidObjects;
    public GameObject mask;
    public Animator playerAnim;
    public AudioSource audioPlayer;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip[] avatarClips;
    public GameObject infoPanel;
    






    public void LoadScene()
    {
        Application.Quit();
    }

    public void LoadInfoCovid()
    {
        StopAllCoroutines();
        StartCoroutine(LoadInfo());
    }

    public void LoadMeasures()
    {
        StopAllCoroutines();
        StartCoroutine(Measures());
    }



    public IEnumerator Measures()
    {
        title.text = "Protective Measures";
        infoPanel.SetActive(true);
        yield return null;
        audioPlayer.clip = clip1;
        audioPlayer.Play();
        titleInfo.text = "Use Mask when you are with other persons";
        mask.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        audioPlayer.clip = clip2;
        audioPlayer.Play();
        titleInfo.text = "Clean Your Hands with Soap or use a hand sanitizer that contains at least 60% alcohol.";
        playerAnim.SetBool("isCovid", true);
        covidObjects.SetActive(true);
        yield return new WaitForSecondsRealtime(10f);
        covidObjects.SetActive(false);
        title.text = "Physical Activity";
        titleInfo.text = "Regular physical activity benefits both the body and mind. It can reduce high blood pressure, help manage weight and reduce the risk of heart disease, stroke, type 2 diabetes, and various cancers - all conditions that can increase susceptibility to COVID-19.";
        audioPlayer.clip = clip3;
        audioPlayer.Play();
        playerAnim.SetBool("isYoga", true);
        yield return new WaitForSecondsRealtime(20f);
        audioPlayer.clip = clip4;
        audioPlayer.Play();
        playerAnim.SetBool("isJumping", true);
        title.text = "How much physical activity is recommended?";
        titleInfo.text = "All children and adolescents should try to perform moderate to vigorous-intensity physical activity This should include activities that strengthen muscle and bone, at least 3 days per week Doing more than 60 minutes of physical activity daily will provide additional health benefits";


    }


    public IEnumerator LoadInfo()
    {
       
        title.text = "Lungs Damage";
        covidObjects.SetActive(false);
        infoPanel.SetActive(false);
        firstInfo.SetActive(true);
        audioPlayer.clip = avatarClips[0];
        audioPlayer.Play();
        yield return new WaitForSecondsRealtime(3f);
        secondInfo.SetActive(true);
        audioPlayer.clip = avatarClips[1];
        audioPlayer.Play();
        yield return new WaitForSecondsRealtime(3f);
        thirdInfo.SetActive(true);
        audioPlayer.clip = avatarClips[2];
        audioPlayer.Play();
        yield return new WaitForSecondsRealtime(3f);
        fourthInfo.SetActive(true);
        audioPlayer.clip = avatarClips[3];
        audioPlayer.Play();
        yield return new WaitForSecondsRealtime(3f);
        fifthInfo.SetActive(true);
        audioPlayer.clip = avatarClips[4];
        audioPlayer.Play();
        yield return new WaitForSecondsRealtime(5f);
        sixtInfo.SetActive(true);
        audioPlayer.clip = avatarClips[5];
        audioPlayer.Play();
    }



}

