using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ServerModels;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;


[System.Serializable]
public class DaysProperties
{
    public string nameDay;
    public int amountDays;
    public int usesDays;
    public string nameActivity;
    public string amountReward;

}


public class ScheduleManager : MonoBehaviour
{

    public Dictionary<string, string> daysActivitys;
    public TextMeshProUGUI mondayText;
    public TextMeshProUGUI tuesdayText;
    public TextMeshProUGUI wendsdayText;
    public TextMeshProUGUI thirstDayText;
    public TextMeshProUGUI fridayText;
    public TextMeshProUGUI weekendText;
    public TextMeshProUGUI mondayReward;
    public TextMeshProUGUI tuesdayReward;
    public TextMeshProUGUI wednesdayReward;
    public TextMeshProUGUI thurstdayReward;
    public TextMeshProUGUI fridayReward;
    public TextMeshProUGUI weekendReward;

    public List<DaysProperties> daysWeek;
    public string activityOne;
    //public string amountRewardOne;
    public string activityTwo;
    public string nameItem;
    public bool hasItem;
    public string mondayInfo;
    public string tuesdayInfo;
    public string wednesdayInfo;
    public string thursdayInfo;
    public string fridayInfo;
    public string weekendInfo;
    public TextMeshProUGUI amountInfo;
    public TextMeshProUGUI challengeInfo;
    public TextMeshProUGUI amountCoins;



    private void Awake()
    {
        //GetDaysData();
    }

    void Start()
    {
        
        
    }


   
    public void GetCurrentDay(string day)
    {
        foreach (DaysProperties currentday in daysWeek)
        {
            if (currentday.nameDay == day)
            {
                MenuManager.menuManager.currentDay = currentday;
            }

        }

    }





    public void GetDaysData()
    {
        GetTitleDataRequest request = new GetTitleDataRequest();
        PlayFabServerAPI.GetTitleData(request, OnResultGetDays, error => { Debug.Log(error.GenerateErrorReport()); });
    }


   

         


    private void OnResultGetDays(GetTitleDataResult result)
    {
        daysActivitys = result.Data;// Set Dictionary Data Start
        MenuManager.menuManager.daysActivity = result.Data;
        // Set The day Activity
        Debug.Log("Succes Update Days");
        mondayText.text = daysActivitys["Monday"];
        //mondayReward.text = daysActivitys["MondayCoins"];
        DaysProperties newDay = new DaysProperties();
        newDay.nameDay = "Monday";
        newDay.amountDays =Int32.Parse(daysActivitys["MondayAmount"]);
        newDay.nameActivity = daysActivitys["Monday"];
        newDay.amountReward = daysActivitys["MondayCoins"];
        daysWeek.Add(newDay);
        tuesdayText.text = daysActivitys["Tuesday"];
        //tuesdayReward.text = daysActivitys["TuesdayCoins"];

        DaysProperties newDay2 = new DaysProperties();
        newDay2.nameDay = "Tuesday";
        newDay2.amountDays = Int32.Parse(daysActivitys["TuesdayAmount"]);
        newDay2.nameActivity = daysActivitys["Tuesday"];
        newDay2.amountReward = daysActivitys["TuesdayCoins"];
        daysWeek.Add(newDay2);
    
        wendsdayText.text = daysActivitys["Wednesday"];
        //wednesdayReward.text = daysActivitys["WednesdayCoins"];
        DaysProperties newDay3 = new DaysProperties();
        newDay3.nameDay = "Wednesday";
        newDay3.amountDays = Int32.Parse(daysActivitys["WednesdayAmount"]);
        newDay3.nameActivity = daysActivitys["Wednesday"];
        newDay3.amountReward = daysActivitys["WednesdayCoins"];
        daysWeek.Add(newDay3);
        thirstDayText.text = daysActivitys["Thursday"];
        //thurstdayReward.text = daysActivitys["ThursdayCoins"];
        DaysProperties newDay4 = new DaysProperties();
        newDay4.nameDay = "Thursday";
        newDay4.amountDays = Int32.Parse(daysActivitys["ThursdayAmount"]);
        newDay4.nameActivity = daysActivitys["Thursday"];
        newDay4.amountReward = daysActivitys["ThursdayCoins"];
        daysWeek.Add(newDay4);
        fridayText.text = daysActivitys["Friday"];
        //fridayReward.text = daysActivitys["FridayCoins"];
        DaysProperties newDay5 = new DaysProperties();
        newDay5.nameDay = "Friday";
        newDay5.amountDays = Int32.Parse(daysActivitys["FridayAmount"]);
        newDay5.nameActivity = daysActivitys["Friday"];
        newDay5.amountReward = daysActivitys["FridayCoins"];
        daysWeek.Add(newDay5);
        weekendText.text = daysActivitys["Weekend"];
        //weekendReward.text = daysActivitys["WeekendCoins"];
        DaysProperties newDay6 = new DaysProperties();
        newDay6.nameDay = "Weekend";
        newDay6.amountDays = Int32.Parse(daysActivitys["WeekendAmount"]);
        newDay6.nameActivity = daysActivitys["Weekend"];
        newDay6.amountReward = daysActivitys["WeekendCoins"];
        daysWeek.Add(newDay6);
        activityOne = daysActivitys["ExtraActivityOne"];
        MenuManager.menuManager.bonusName = daysActivitys["ExtraActivityOne"];
        activityTwo = daysActivitys["ExtraActivityTwo"];

        PlayFabOrder.instance.isGetDataDay = true;
    }

    public void GrantItemToPlayer()
    {        
        GrantItemsToUserRequest request = new GrantItemsToUserRequest();
        request.CatalogVersion = "ExtraItem";
        request.PlayFabId = MenuManager.menuManager.playFabIdUser;
        request.ItemIds = new List<string>(){"ExtraActivity"};
        
        PlayFabServerAPI.GrantItemsToUser(request, OnResultRequest, error => { Debug.Log(error.GenerateErrorReport()); });
    }
    private void OnResultRequest(GrantItemsToUserResult result)
    {
        Debug.Log("ItemGranted");
    }

    public void GetItem()
    {   

        GetUserInventoryRequest request = new GetUserInventoryRequest();
        request.PlayFabId = MenuManager.menuManager.playFabIdUser;
        PlayFabServerAPI.GetUserInventory(request, OnGetInventoryResult, error => { Debug.Log(error.GenerateErrorReport()); });
    }

    

    private void OnGetInventoryResult(GetUserInventoryResult result)
    {

        Debug.Log("Get Item Succes");
        foreach (ItemInstance item in result.Inventory)
        {
            if(item.ItemId == "ExtraActivity")
               {
                hasItem = true;
                MenuManager.menuManager.itemUseId = item.ItemInstanceId;
                MenuManager.menuManager.usesItem = item.RemainingUses;
                                                          
                Debug.Log(MenuManager.menuManager.usesItem);
                PlayFabOrder.instance.isGetItem = true;
                //PlayFabManager.instance.GetDayUsed();
                return;
               }
            

        }
        PlayFabOrder.instance.isGetItem = true;
        //PlayFabManager.instance.GetDayUsed();






    }

    public void SetDaysInfo(string nameDay)
    {
        amountInfo.text = daysActivitys[nameDay];
        switch (nameDay)
        {
            case "MondayAmount":
                {
                    challengeInfo.text = mondayInfo;
                    amountCoins.text = daysActivitys["MondayCoins"];
                    break;
                };
            case "TuesdayAmount":
                {
                    challengeInfo.text = tuesdayInfo;
                    amountCoins.text = daysActivitys["TuesdayCoins"];
                    break;
                }
            case "WednesdayAmount":
                {
                    challengeInfo.text = wednesdayInfo;
                    amountCoins.text = daysActivitys["WednesdayCoins"];
                    break;
                }
            case "ThursdayAmount":
                {
                    challengeInfo.text = thursdayInfo;
                    amountCoins.text = daysActivitys["ThursdayCoins"];
                    break;
                }
            case "FridayAmount":
                {
                    challengeInfo.text = fridayInfo;
                    amountCoins.text = daysActivitys["FridayCoins"];
                    break;
                }

            case "WeekendAmount":
                {
                    challengeInfo.text = weekendInfo;
                    amountCoins.text = daysActivitys["WeekendCoins"];
                    break;
                }
            

        }



    }


    public void UseItem()
    {
        ConsumeItemRequest request = new ConsumeItemRequest();
        request.ConsumeCount = 1;
        request.PlayFabId = MenuManager.menuManager.playFabIdUser;
        request.ItemInstanceId = MenuManager.menuManager.itemUseId;

        PlayFabServerAPI.ConsumeItem(request, result => { Debug.Log("Consume Item"); }, error => { Debug.Log(error.GenerateErrorReport()); });
    }



}
