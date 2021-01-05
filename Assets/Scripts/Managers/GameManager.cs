using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;




[System.Serializable]
public class KindOfActivity
{
    public int totalSeries;
    public int amountSeries;
    public int totalActions;
    public int amountActions;
    public string nameActivity;
    public string nameAnim;// Name Anim in Animator
    public string infoActivity;
    public int amountReward;
    public string dayOfActivity;
    public bool isWithTime;
    public bool isRepeat;
    public float amountTime;
    public bool isFinish;
    public bool isStart;
    public bool isPlaying;
    public int totalTime;
    public AudioClip audioActivity;
    public kindOfSerie kindOfSerie;
    
}

public enum kindOfSerie
{
    isPose,
    isDinamic
}




public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI amountGold;
    public TextMeshProUGUI amountSeries;
    public List<KindOfActivity> activitys;
    public KindOfActivity activitySelect;
    public Button buttonAction;
    public GameObject backPack;
    public TextMeshProUGUI nameActivity;
    public TextMeshProUGUI namePlayer;
    public TextMeshProUGUI infoActivity;
    public TextMeshProUGUI nameActivityPanel;
    public AudioSource audioActivity;
    public AudioClip clipACtivity;
    public GameObject staticPanel;
    public GameObject dinamicPanel;
    public TMP_Dropdown secondsDropdownChild;
    public TMP_Dropdown secondsDropdownParent;
    public TextMeshProUGUI finalAmount;
    public TextMeshProUGUI finalNameActivity;
    public TMP_Dropdown amountActivitysChild;
    public TMP_Dropdown amountActivitysParent;
    public TextMeshProUGUI parentNamePanel;
    public TextMeshProUGUI childNamePanel;
    

    // Start is called before the first frame update
    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        GetActivityName();
        GetDropDownPanel();
    }

    private void Start()
    {
        amountGold.text = PlayFabCurrency.playFabCurrency.amountCoins.ToString();
        SetName();
        //finalAmount.text = activitySelect.amountReward.ToString();
         //finalNameActivity.text = activitySelect.nameActivity;
        
    }
    public void DisableButton()
    {
        buttonAction.interactable = false;
    }
    private void GetActivityName()// 
    {
        foreach (KindOfActivity activity in activitys)
        {
            if ((activity.nameActivity == MenuManager.menuManager.nameActivity) && (activity.dayOfActivity == MenuManager.menuManager.dayActivity))
            {

                activitySelect = activity;
                                
                //amountText.text = activitySelect.amountActions.ToString() + "/" + activitySelect.totalActions.ToString();
                amountSeries.text = activitySelect.amountSeries.ToString() + "/" + activitySelect.totalSeries.ToString();
                return;
            }
        }



    }


    public void ChangeTextSerie()
    {
        amountSeries.text = activitySelect.amountSeries.ToString() + "/" + activitySelect.totalSeries.ToString();

    }


    private void GetDropDownPanel()
    {
        FillDropdown();
        if (activitySelect.kindOfSerie == kindOfSerie.isPose)
        {

            staticPanel.SetActive(true);
        }

        if (activitySelect.kindOfSerie == kindOfSerie.isDinamic)
        {
            dinamicPanel.SetActive(true);
        }


    }
    private void FillDropdown()
    {
        
        List<TMP_Dropdown.OptionData> newList = new List<TMP_Dropdown.OptionData>();
        for (int i = 1; i < 60; i++)
        {
            TMP_Dropdown.OptionData newOption = new TMP_Dropdown.OptionData();
            newOption.text = i.ToString();
            newList.Add(newOption);

        }
        secondsDropdownChild.AddOptions(newList);
        secondsDropdownParent.AddOptions(newList);

        List<TMP_Dropdown.OptionData> secondList = new List<TMP_Dropdown.OptionData>();
        for (int i = 1; i < 80; i++)
        {
            TMP_Dropdown.OptionData secondOption = new TMP_Dropdown.OptionData();
            secondOption.text = i.ToString();
            secondList.Add(secondOption);
        }
        amountActivitysChild.AddOptions(secondList);
        amountActivitysParent.AddOptions(secondList);


    }

    private void SetName()
    {
        nameActivity.text = MenuManager.menuManager.nameActivity;
        namePlayer.text = MenuManager.menuManager.playerName;
        infoActivity.text = activitySelect.infoActivity;
        nameActivityPanel.text = activitySelect.nameActivity;
        Invoke("PlayAudio", 1f);
        childNamePanel.text = MenuManager.menuManager.childFormName;
        parentNamePanel.text = MenuManager.menuManager.parentFormName;
        
        
    }

    private void PlayAudio()
    {
        audioActivity.clip = activitySelect.audioActivity;
        audioActivity.Play();


    }

    private void CheckActivity()
    {
       
    }


    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
