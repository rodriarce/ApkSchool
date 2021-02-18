using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ServerModels;
using System;
using UnityEngine.UI;


public class PlayFabOrder : MonoBehaviour
{
    public static PlayFabOrder instance;
    public PlayFabRegister playFabRegister;
    public ScheduleManager scheduleManager;
    public PushFirebase pushFirebase;
    public LeaderBoard leaderBoard;
    public Button buttonPlay;
    public Button buttonCovid;
    public bool isLogin;
    public Action loginPlayer;
    public bool isGetDataDay;
    public Action getDataDay;
    public bool isCurrentDay;
    public Action currentDay;
    public Action getItem;
    public bool isGetItem;
    public Action getDayUsed;
    public bool isGetDayUsed;
    public bool hasGrantDay;
    public Action grantDay;
    public bool isGrantDay;
    public bool isGetStats;
    public Action getStats;
    public bool isGetLeaderBoard;
    public Action getLeaderBoard;
    public bool isRegister;
    public bool isNameUpdate;
    public Action updateName;
    public Action loadFirebase;
    public bool isFirebaseLoad;
   



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ServerRequest());
    }

    public IEnumerator ServerRequest()
    {
        while (!isLogin)
        {
            if (loginPlayer == null)
            {
                loginPlayer = playFabRegister.LoginUser;
                loginPlayer.Invoke();
            }
           yield return null;
        }
        while (isRegister)
        {

            yield return null;
        }
      

        while (!isGetDataDay)
        {
            if (getDataDay == null)
            {
                getDataDay = scheduleManager.GetDaysData;
                getDataDay.Invoke();
            }
            yield return null;
        }



        while (!isCurrentDay)
        {
            if (currentDay == null)
            {
                currentDay = PlayFabManager.instance.GetCurrentDay;
                currentDay.Invoke();
            }
            yield return null;
        }



        while (!isGetItem)
        {
            if (getItem == null)
            {
                getItem = scheduleManager.GetItem;
                getItem.Invoke();
            }
            yield return null;
        }
        while (!isGetDayUsed)
        {
            if (getDayUsed == null)
            {
                getDayUsed = PlayFabManager.instance.GetDayUsed;
                getDayUsed.Invoke();
            }
            yield return null;
        }

        while (hasGrantDay)
        {
            if (grantDay == null)
            {
                grantDay = PlayFabManager.instance.GrantDay;
                grantDay.Invoke();
            }
            yield return null;
        }

        while (!isGetStats)
        {
            if (getStats == null)
            {
                getStats = PlayFabManager.instance.GetPlayerStatistics;
                getStats.Invoke();
            }
            yield return null;
        }


        while (!isGetLeaderBoard)
        {
            if (getLeaderBoard == null)
            {
                getLeaderBoard = leaderBoard.GetLeaderBoard;
                getLeaderBoard.Invoke();
                ActiveButtons();
            }
            yield return null;
        }
        while (!isFirebaseLoad)
        {
            if (loadFirebase == null)
            {
                loadFirebase = pushFirebase.SetFirebase;
                loadFirebase.Invoke();
                ActiveButtons();
            }
            yield return null;
        }



    }

    public void ActiveButtons()
    {
        buttonCovid.interactable = true;
    }
}
