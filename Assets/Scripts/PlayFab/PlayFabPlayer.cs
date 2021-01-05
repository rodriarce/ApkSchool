using PlayFab;
using PlayFab.ServerModels;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayFabPlayer : MonoBehaviour
{


    public int amountVictorys;
    public int amountGold;
    public PlayerController playerController;
    public TextMeshProUGUI amountGoldText;
    public TMP_Dropdown amountActions;
    public TMP_Dropdown amountActionsParent;
    public TMP_Dropdown amountMinutes;
    public TMP_Dropdown amountSeconds;
    public TMP_Dropdown amountMinutesParent;
    public TMP_Dropdown amountSecondsParent;

    public int totalSeconds;
    public int currentAmount;
    public int currentAmountParent;
    public int coinsParent;
    public int coinsChild;
    public int currentParentCoins;
    public int currentChildCoins;
    public string winner;



    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        //GrantPlayerDay();
        //UseItem();
        GetPlayerStats();

    }


    public void GrantPlayerDay()
    {
        if (MenuManager.menuManager.amountUses >= MenuManager.menuManager.currentDay.amountDays)
        {
            playerController.isGrantDay = true;
            return;
        }

        UpdateUserDataRequest request = new UpdateUserDataRequest();
        Dictionary<string, string> isUsed = new Dictionary<string, string>();
        isUsed.Add("lastLogin", MenuManager.menuManager.dayActivity);// Last Day Login
        MenuManager.menuManager.amountUses++;

        if (IncreaseUses())// Check amount of ACtivitys
        {
            isUsed.Add("isUsed", "true");
            Debug.Log("true");
        }
        else
        {
            isUsed.Add("isUsed", "false");
            Debug.Log("false");
        }


        isUsed.Add("amountUses", MenuManager.menuManager.amountUses.ToString());// Add Amount Activitys
        request.Data = isUsed;
        request.PlayFabId = MenuManager.menuManager.playFabIdUser;

        PlayFabServerAPI.UpdateUserData(request, OnGrantDay, OnErrorUpdate);

    }

    private void OnErrorUpdate(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    private bool IncreaseUses()
    {

        if (MenuManager.menuManager.amountUses >= MenuManager.menuManager.currentDay.amountDays)
        {
            return true;
        }
        else
        {

            return false;
        }


    }



    private void OnGrantDay(UpdateUserDataResult result)
    {
        Debug.Log("Update Last Day");
        playerController.isGrantDay = true;
    }
    public void UseItem()
    {

        if (MenuManager.menuManager.itemUseId == "")
        {
            playerController.isUseItem = true;
            return;
        }
        ConsumeItemRequest request = new ConsumeItemRequest();
        request.ConsumeCount = 1;
        request.PlayFabId = MenuManager.menuManager.playFabIdUser;
        request.ItemInstanceId = MenuManager.menuManager.itemUseId;

        PlayFabServerAPI.ConsumeItem(request, OnResultUseItem, error => { Debug.Log(error.GenerateErrorReport()); });
    }


    private void OnResultUseItem(ConsumeItemResult result)
    {
        Debug.Log("Succes Use Item");
        playerController.isUseItem = true;
    }



    public void GetPlayerStats()
    {
        GetPlayerStatisticsRequest request = new GetPlayerStatisticsRequest();
        request.PlayFabId = MenuManager.menuManager.playFabIdUser;
        List<string> names = new List<string>() { "Wins", "Gold", MenuManager.menuManager.nameActivity, "ParentCoins","ChildCoins" };

        request.StatisticNames = names;

        PlayFabServerAPI.GetPlayerStatistics(request, OnGetStatsResult, error => { Debug.Log(error.GenerateErrorReport()); });

    }

    private void OnGetStatsResult(GetPlayerStatisticsResult result)
    {
        foreach (StatisticValue stat in result.Statistics)
        {
            if (stat.StatisticName == "Wins")
            {
                amountVictorys = stat.Value;
            }
            if (stat.StatisticName == "Gold")
            {
                amountGold = stat.Value;
                amountGoldText.text = amountGold.ToString();
            }
            if (stat.StatisticName == MenuManager.menuManager.nameActivity)
            {
                currentAmount = stat.Value;
            }
            if (stat.StatisticName == "ParentCoins")
            {
                currentParentCoins = stat.Value;
            }
            if (stat.StatisticName == "ChildCoins")
            {
                currentChildCoins = stat.Value;
            }

        }

    }


    
   


    public void AddCurreny(int amount)
    {
        AddUserVirtualCurrencyRequest request = new AddUserVirtualCurrencyRequest();
        request.PlayFabId = PlayFabManager.instance.playFabId;
        request.Amount = amount;
        request.VirtualCurrency = "US";
        PlayFabServerAPI.AddUserVirtualCurrency(request, OnAddCurrencySucces, OnErrorCurrency);


    }

    private void OnAddCurrencySucces(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Succes Add Currency");
        playerController.isAddCurrency = true;
    }
    private void OnErrorCurrency(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    public void UpdateStatistic()
    {


        if (GameManager.gameManager.activitySelect.kindOfSerie == kindOfSerie.isPose)
        {
            int timeParent = GetSeconds(amountMinutesParent, amountSecondsParent);
            int timeChild = GetSeconds(amountMinutes, amountSeconds);

            GetWinner(timeParent, timeChild);
                    



        }

        if (GameManager.gameManager.activitySelect.kindOfSerie == kindOfSerie.isDinamic)
        {
            int amountParent = GetActivities(amountActionsParent);
            int amountChild = GetActivities(amountActions);
            GetWinner(amountParent, amountChild);
                     

        }
        UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest();
        request.PlayFabId = MenuManager.menuManager.playFabIdUser;
        StatisticUpdate parentAmount = new StatisticUpdate();
        currentParentCoins += coinsParent;
        parentAmount.StatisticName = "ParentCoins";
        parentAmount.Value = currentParentCoins;
        StatisticUpdate childAmount = new StatisticUpdate();
        currentChildCoins += coinsChild;
        childAmount.StatisticName = "ChildCoins";
        childAmount.Value = currentChildCoins;
        StatisticUpdate playerWins = new StatisticUpdate();
        playerWins.StatisticName = "Wins";
        amountVictorys++;
        playerWins.Value = amountVictorys;
        StatisticUpdate amount = new StatisticUpdate();
        amount.StatisticName = "Gold";
        amountGold = coinsChild + coinsParent + amountGold;
        amount.Value = amountGold;
        List<StatisticUpdate> newStatistics = new List<StatisticUpdate>();
        newStatistics.Add(parentAmount);
        newStatistics.Add(childAmount);
        newStatistics.Add(playerWins);
        newStatistics.Add(amount);

        request.Statistics = newStatistics;
        PlayFabServerAPI.UpdatePlayerStatistics(request, OnUpdateStat, OnUpdateError);

    }
    private void OnUpdateStat(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Succes Update Stat");
        playerController.isStatsUpdate = true;
        amountGoldText.text = amountGold.ToString();
    }
    private void OnUpdateError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    private int GetSeconds(TMP_Dropdown minutesAction, TMP_Dropdown secondsAction)
    {

        int seconds = Int32.Parse(secondsAction.options[secondsAction.value].text);
        int minutes = Int32.Parse(minutesAction.options[minutesAction.value].text);
        int minutesToSeconds = minutes * 60;
        totalSeconds = minutesToSeconds + seconds;
        return totalSeconds;


    }

    private int GetActivities(TMP_Dropdown amountActivitys)
    {

        string optionSelect = amountActivitys.options[amountActivitys.value].text;
        return Int32.Parse(optionSelect);
    }

    private void GetWinner(int parent, int child)
    {
        if (parent > child)
        {
            coinsParent = GameManager.gameManager.activitySelect.amountReward;
            coinsChild = GameManager.gameManager.activitySelect.amountReward - 15;
            winner = "Parent";

        }
        if (child > parent)
        {
            coinsParent = GameManager.gameManager.activitySelect.amountReward - 15;
            coinsChild = GameManager.gameManager.activitySelect.amountReward;
            winner = "Child";

        }
        if (child == parent)
        {
            winner = "Both";
            coinsParent = GameManager.gameManager.activitySelect.amountReward - 10;
            coinsChild = GameManager.gameManager.activitySelect.amountReward - 10;
        }

    }





}// End Script


