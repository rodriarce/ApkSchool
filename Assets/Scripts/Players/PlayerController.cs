using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;




public class PlayerController : MonoBehaviour
{
    public Animator playerAnim;
    public int totalSeries = 3;
    private int amountSeries = 0;
    public int totalActions;
    public int amountActions = 0;
    public bool isChildren;
    public bool isParent;
    public KindOfActivity selectActivity;
    private bool isPlayingAnim;
    public float timer;
    public PlayFabPlayer playFabPlayer;
    public GameObject backPack;
    public GameObject obstacle;
    public GameObject rewardPanel;
    public GameObject panelInput;
    public GameObject panelFinishLevel;
    public AudioSource audioPlayer;
    public AudioClip firtSerie;
    public AudioClip secondSerie;
    public AudioClip thirdSerie;
    public bool isAddCurrency;
    public bool isUpdateStats;
    public bool isGrantDay;
    public bool isUseItem;
    public bool isStatsUpdate;
    private bool firstAction;
    private bool thirdAction;
    private bool fourthAction;
    private bool fifthAction;
    public Dropdown dropDownMenu;
    public Button buttonSend;
    public bool isAnimStart;
    public Button buttonFinish;
    public GameObject finishPanel;
    public Button buttonDinamic;
    public TextMeshProUGUI finishAmounChild;
    public TextMeshProUGUI finishAmountParent;
    public GameObject winnerParent;
    public GameObject winnerChild;
    public Button buttonReward;
    private bool startPoseAnim;
    
    



    
 

    //public KindOfActivity kindofActivity;
    // Start is called before the first frame update
    void Start()
    {
        SetAmountActivitys();
        //playFabPlayer.UpdatePlayerStats(10);
        //StartCoroutine(UpdateStats());
        buttonSend.onClick.AddListener(NextAction);
        buttonDinamic.onClick.AddListener(NextAction);
        buttonFinish.onClick.AddListener(OnStopPose);
        buttonReward.onClick.AddListener(OnButtonReward);
        //finishAmount.text = selectActivity.amountReward.ToString();
        //ShowInfo();
        

    }

    private void SetAmountActivitys()
    {
        selectActivity = GameManager.gameManager.activitySelect;
    }


    public void OnPlayAnim()
    {

        if (selectActivity.kindOfSerie == kindOfSerie.isPose)
        {
            finishPanel.SetActive(true);
            startPoseAnim = true;
            StartCoroutine(CounterPose());
            
        }

        audioPlayer.clip = firtSerie;
        audioPlayer.Play();

        playerAnim.SetBool(selectActivity.nameAnim, true);
        selectActivity.isPlaying = true;
        if (selectActivity.kindOfSerie == kindOfSerie.isDinamic)
        {
            StartCoroutine(IncreaseTimer());
        }
    }
    public void StartAnim()
    {
        isAnimStart = true;
    }

    public void OnStopPose()
    {
        startPoseAnim = false;
        StopCoroutine(CounterPose());
        playerAnim.SetBool(selectActivity.nameAnim, false);
        selectActivity.isPlaying = false;
        panelInput.SetActive(true);
        //playerAnim.SetBool("isFinish", true);
        //StartCoroutine(UpdateStats());
        


    }

    public void OnButtonReward()
    {
        if (selectActivity.kindOfSerie == kindOfSerie.isDinamic)
        {
            if (selectActivity.amountSeries != selectActivity.totalSeries)
            {
                isAnimStart = false;
                PlayAnim();
                //Invoke("PlayAnim", 10f);
                rewardPanel.SetActive(false);
            }
            else
            {
                rewardPanel.SetActive(false);
                panelFinishLevel.SetActive(true);
                MenuManager.menuManager.isFinishLevel = true;
            }

        }
        if (selectActivity.kindOfSerie == kindOfSerie.isPose)
        {
            if (selectActivity.amountSeries != selectActivity.totalSeries)
            {
                isAnimStart = false;
                PlayAnim();
                rewardPanel.SetActive(false);
                
            }
            else
            {
                rewardPanel.SetActive(false);
                panelFinishLevel.SetActive(true);
                MenuManager.menuManager.isFinishLevel = true;
                
            }
            

        }


     

    }
    

    private IEnumerator CounterPose()
    {
        while (startPoseAnim)
        {
            selectActivity.amountTime++;
            GameManager.gameManager.amountText.text = selectActivity.amountTime.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
    }
        
    

    private IEnumerator IncreaseTimer()
    {
        while (!isAnimStart)
        {
            yield return null;
        }

        while (selectActivity.amountTime < selectActivity.totalTime)
        {
            if (isChildren)
            {
                selectActivity.isPlaying = true;

                selectActivity.amountTime += 1f;
                GameManager.gameManager.amountText.text = selectActivity.amountTime.ToString() + "/" + selectActivity.totalTime.ToString();
                Debug.Log((int)selectActivity.amountTime);
                
            }
            yield return new WaitForSecondsRealtime(1f);
        }
      
        while (selectActivity.isStart)
        {
            selectActivity.isStart = false;
            playerAnim.SetBool(selectActivity.nameAnim, false);
            selectActivity.isPlaying = false;
            panelInput.SetActive(true);
            yield return null;

        }
        while (selectActivity.amountTime == selectActivity.totalTime)
        {
            
            panelInput.SetActive(true);
            yield return null;
        }





        // Set to Idel When Finish
    }
    public void CheckTime()
    {
        return;
        if ((!selectActivity.isWithTime) || (selectActivity.isStart))
        {
            return;
        }
        
        
        if (isChildren)
        {
            selectActivity.amountTime = 0;
            //selectActivity.amountSeries++;
            GameManager.gameManager.amountSeries.text = selectActivity.amountSeries.ToString() + "/" + selectActivity.totalSeries.ToString();
        }

        

        if (selectActivity.amountSeries == selectActivity.totalSeries)
        {
            selectActivity.isPlaying = false;

            playerAnim.SetBool(selectActivity.nameAnim, false);
            playerAnim.SetBool("isFinish", true);
            Debug.Log("Finish Activity");
            selectActivity.isFinish = true;

            
            selectActivity.isStart = true;
            if (isChildren)
            {
                rewardPanel.SetActive(true);
                AudioScene.audioScene.PlayAudioVictory();
                StartCoroutine(UpdateStats());
              }
            ShowInputPanel();
            return;
        }
        selectActivity.isStart = true;
        ShowInputPanel();
        //Invoke("PlayAnim", 10f);

    }

    private void ShowInputPanel()
    {
        panelInput.SetActive(true);
        
    }
    public void NextAction()
    {
        StopCoroutine(IncreaseTimer());
        SetBoolsToFalse();
        if (selectActivity.kindOfSerie == kindOfSerie.isPose)
        {
            selectActivity.amountTime = 0;
            selectActivity.amountSeries++;
            GameManager.gameManager.ChangeTextSerie();


            if (selectActivity.amountSeries == selectActivity.totalSeries)// Finish Series
            {
                buttonFinish.interactable = false;
                selectActivity.isFinish = true;

                playerAnim.SetBool(selectActivity.nameAnim, false);
                playerAnim.SetBool("isFinish", true);
                 AudioScene.audioScene.PlayAudioVictory();

                //PlayFabCurrency.playFabCurrency.AddCurreny(selectActivity.amountReward);
                // playFabPlayer.UpdatePlayerStats(selectActivity.amountReward);
                //GameManager.gameManager.amountGold.text = amount.ToString();
                panelInput.SetActive(false);
                //rewardPanel.SetActive(true);
                //playFabPlayer.GrantPlayerDay();
                //playFabPlayer.UseItem();// Use Bonus Item
                isUpdateStats = false;
                StartCoroutine(UpdateStats());
                return;
            }



            //selectActivity.isFinish = true;
            playerAnim.SetBool(selectActivity.nameAnim, false);

            
            panelInput.SetActive(false);
            //rewardPanel.SetActive(true);
            StartCoroutine(UpdateStats());
            return;

        }


        if (selectActivity.kindOfSerie == kindOfSerie.isDinamic)
        {
            selectActivity.amountTime = 0;
            selectActivity.amountSeries++;
            GameManager.gameManager.ChangeTextSerie();


            if (selectActivity.amountSeries == selectActivity.totalSeries)// Finish Series
            {

                selectActivity.isFinish = true;

                playerAnim.SetBool(selectActivity.nameAnim, false);
                playerAnim.SetBool("isFinish", true);
                Debug.Log("Finish Activity");
                AudioScene.audioScene.PlayAudioVictory();

                //PlayFabCurrency.playFabCurrency.AddCurreny(selectActivity.amountReward);
                // playFabPlayer.UpdatePlayerStats(selectActivity.amountReward);
                //GameManager.gameManager.amountGold.text = amount.ToString();
                panelInput.SetActive(false);
                //rewardPanel.SetActive(true);
                //playFabPlayer.GrantPlayerDay();
                //playFabPlayer.UseItem();// Use Bonus Item
                isUpdateStats = false;
                StartCoroutine(UpdateStats());
                return;
            }
            StartCoroutine(UpdateStats());

        }

        
        panelInput.SetActive(false);
        
        //playFabPlayer.UpdateStatistic();
    }



    private void PlayAnim()
    {


        playerAnim.SetBool(selectActivity.nameAnim, true);// Start the Animation Again
        selectActivity.isStart = true;
        selectActivity.isPlaying = true;
        if (selectActivity.kindOfSerie == kindOfSerie.isPose)
        {
            startPoseAnim = true;
            StartCoroutine(CounterPose());
        }
        if (selectActivity.kindOfSerie == kindOfSerie.isDinamic)
        {
           StartCoroutine(IncreaseTimer());
        }
        
        if (selectActivity.amountSeries == 1)
        {
            audioPlayer.clip = secondSerie;
            audioPlayer.Play();
        }
        if (selectActivity.amountSeries == 2)
        {
            audioPlayer.clip = thirdSerie;
            audioPlayer.Play();
        }
        
    }



    public void ActiveBackPack()
    {
       
            backPack.SetActive(true);
        
    }

    public void ActiveObstacle()
    {
        if (!obstacle.activeInHierarchy)
        {
            obstacle.SetActive(true);
        }
    }



    public void IncreaseAnim()
    {
        return;   
        if (isParent)
        {


            if (selectActivity.amountSeries == selectActivity.totalSeries)// Finish Series
            {
                playerAnim.SetBool(selectActivity.nameAnim, false);
                playerAnim.SetBool("isFinish", true);


            }

        }

        if (isChildren)
        {
            selectActivity.amountActions++;
            GameManager.gameManager.amountText.text = selectActivity.amountActions.ToString() + "/" + selectActivity.totalActions.ToString();
            GameManager.gameManager.amountSeries.text = selectActivity.amountSeries.ToString() + "/" + selectActivity.totalSeries.ToString();
            
            if (selectActivity.amountActions == selectActivity.totalActions)
            {

                
                selectActivity.amountSeries++;
                GameManager.gameManager.amountSeries.text = selectActivity.amountSeries.ToString() + "/" + selectActivity.totalSeries.ToString();
                playerAnim.SetBool(selectActivity.nameAnim, false);
                selectActivity.isPlaying = false;
                Invoke("StopAnimations", 10f);

                if (selectActivity.amountSeries == selectActivity.totalSeries)// Finish Series
                {

                    selectActivity.isFinish = true;

                    playerAnim.SetBool(selectActivity.nameAnim, false);
                    playerAnim.SetBool("isFinish", true);
                    Debug.Log("Finish Activity");
                    AudioScene.audioScene.PlayAudioVictory();

                    //PlayFabCurrency.playFabCurrency.AddCurreny(selectActivity.amountReward);
                     // playFabPlayer.UpdatePlayerStats(selectActivity.amountReward);
                    //GameManager.gameManager.amountGold.text = amount.ToString();
                    rewardPanel.SetActive(true);
                    //playFabPlayer.GrantPlayerDay();
                    //playFabPlayer.UseItem();// Use Bonus Item
                    StartCoroutine(UpdateStats());
                }
                else
                {

                    selectActivity.amountActions = 0;


                }
            }
        }



        //a
    }

    private void StopAnimations()
    {
        playerAnim.SetBool(selectActivity.nameAnim, true);
        selectActivity.isPlaying = true;
        if (selectActivity.amountSeries == 1)
        {
            audioPlayer.clip = secondSerie;
            audioPlayer.Play();
        }
        if (selectActivity.amountSeries == 2)
        {
            audioPlayer.clip = thirdSerie;
            audioPlayer.Play();
        }
    }

    private void SetBoolsToFalse()
    {
        firstAction = false;
        thirdAction = false;
        fourthAction = false;
        fifthAction = false;
        isGrantDay = false;
        //isUseItem = false;
        isStatsUpdate = false;


    }


    public IEnumerator UpdateStats()
    {

        while(!isAddCurrency)
        {
            if (!firstAction)
            {
                playFabPlayer.AddCurreny(selectActivity.amountReward);
                firstAction = true;
            }
            yield return null;
        }
      
        while (!isGrantDay)
        {
            if (!thirdAction)
            {
                playFabPlayer.GrantPlayerDay();
                thirdAction = true;
            }
            yield return null;
        }

        while (!isUseItem)
        {
            if (!fourthAction)
            {
                playFabPlayer.UseItem();
                fourthAction = true;
            }
            yield return null;
        }
        while (!isStatsUpdate)
        {
            if (!fifthAction)
            {
                playFabPlayer.UpdateStatistic();
                fifthAction = true;

            }
            yield return null;
        }
        if (isStatsUpdate)
        {
            SetRewardPanel();
            yield return null;
        }
        
    }

    private void SetRewardPanel()
    {
        //playerAnim.SetBool("isFinish", true);
        Debug.Log("Finish Activity");
        AudioScene.audioScene.PlayAudioVictory();
        winnerParent.SetActive(false);
        winnerChild.SetActive(false);
        Debug.Log("RewardPanel");
        finishAmounChild.text = playFabPlayer.coinsChild.ToString();
        finishAmountParent.text = playFabPlayer.coinsParent.ToString();
        if (playFabPlayer.winner == "Parent")
        {
            winnerParent.SetActive(true);
        }
        if (playFabPlayer.winner == "Child")
        {
            winnerChild.SetActive(true);
        }
        if (playFabPlayer.winner == "Both")
        {
            winnerChild.SetActive(true);
            winnerParent.SetActive(true);

        }


        rewardPanel.SetActive(true);

    }



    void Update()
    {

    }
}
