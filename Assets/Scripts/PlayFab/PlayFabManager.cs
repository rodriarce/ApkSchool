using PlayFab;
using PlayFab.ServerModels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class PlayFabManager : MonoBehaviour
{
    public static PlayFabManager instance;
    public string userName;
    public string playFabId;
    [Header("Parent")]
    public InputField parentName;
    //public InputField parentLastName;
    //public Dropdown dayParent;
    //public Dropdown monthParent;
    //public Dropdown yearParent;
    [Header("Child")]
    public InputField childName;
    //public InputField childLastName;
    //public Dropdown dayChild;
    //public Dropdown monthChild;
    //public Dropdown yearChild;
    //public InputField incPostal;
    private DateTime dateTime;
    public string currentDay;
    
   
    //private Dictionary<string, string> userData = new Dictionary<string, string>();
    public Text textResult;
    public GameObject panelGame;
    public GameObject panelEtnic;
    public GameObject panelForm;
    public ToggleGroup toggleGroup;
    private string etniceName;
    public InputField otherInput;
    public string lastDayEnter;
    public bool isDayUsed;
    public Button dayActivity;
    public TextMeshProUGUI coinsPlayer;
    public TextMeshProUGUI amountWins;
    public ScheduleManager scheduleManager;
    public TextMeshProUGUI amountUsesText;
    public TextMeshProUGUI extraText;
    public Button extraBonus;
    public Dropdown genderDropdown;
    public string gender;
    public PlayerAvatar playerAvatar;
    public TextMeshProUGUI titleActivity;
    
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
        
    }

    public void GetCurrentDay()
    {
        GetTimeRequest request = new GetTimeRequest();
        PlayFabServerAPI.GetTime(request, OnTimeResult, OnErrorUpdate);
        
    }

    private void OnTimeResult(GetTimeResult result)
    {
        
        dateTime = result.Time;
        Debug.Log(dateTime.DayOfWeek);
        currentDay = dateTime.DayOfWeek.ToString();

        if ((currentDay == "Saturday") || (currentDay == "Sunday"))
        {
            currentDay = "Weekend";
        }


        MenuManager.menuManager.dayActivity = currentDay;
        MenuManager.menuManager.SetDayActivity();
        //GetDayUsed();
        scheduleManager.GetCurrentDay(currentDay);
        PlayFabOrder.instance.isCurrentDay = true;
        titleActivity.text = MenuManager.menuManager.nameActivity;
    }


    public void OnUpdateForm()
    {
        //OnClickEtnice();
        UpdateUserDataRequest request = new UpdateUserDataRequest();
        request.PlayFabId = playFabId;
        request.Data = SetDataUser();// Dictionary with Data User Form to Update Database
        PlayFabServerAPI.UpdateUserData(request, UpdateUserResult, OnErrorUpdate);// Update User Data
        
    }


    public void OnUpdateEtnice()
    {
        OnClickEtnice();
        UpdateUserDataRequest request = new UpdateUserDataRequest();
        request.PlayFabId = playFabId;
        request.Data = SetEtniceData();// Dictionary with Data User Form to Update Database
        PlayFabServerAPI.UpdateUserData(request, OnResultEtnice, OnErrorUpdate);// Upda
        



    }

    


    private void OnResultEtnice(UpdateUserDataResult result)
    {
        Debug.Log("Succes Form");
        
        
        panelEtnic.SetActive(false);
        
        
    }



    private void UpdateUserResult(UpdateUserDataResult result)
    {
        textResult.text = "Data Load";
        Debug.Log("Form Update");
        PlayFabOrder.instance.isRegister = false;
        PlayFabOrder.instance.isNameUpdate = true;
        panelGame.SetActive(true);
        UpdatePlayerGender(gender);
        playerAvatar.SetImagePlayer(gender);
        panelForm.SetActive(false);


    }

    private void OnErrorUpdate(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        textResult.text = "Check The Form";
        
        
        
    }


    


    private void OnClickEtnice()
    {
        Toggle nameEtnice = toggleGroup.GetFirstActiveToggle();
        etniceName = nameEtnice.GetComponentInChildren<Text>().text;
        if (etniceName == "Other")
        {
            etniceName = otherInput.text;
        }
    }

    private Dictionary<string, string> SetDataUser()
    {


        Dictionary<string, string> userData = new Dictionary<string, string>();

        // Parent Data
        userData.Add("parentName", parentName.text);
        //userData.Add("parentLastName", parentLastName.text);
        //userData.Add("dayParent", dayParent.itemText.text);
        //userData.Add("monthParent", monthParent.itemText.text);
        //userData.Add("yearParent", yearParent.itemText.text);
        // Children Data
        userData.Add("childName", childName.text);
        gender = genderDropdown.options[genderDropdown.value].text;
        userData.Add("Gender", gender);


        //userData.Add("childLastName", childLastName.text);
        //userData.Add("dayChild", dayChild.itemText.text);
        //userData.Add("monthChild", monthChild.itemText.text);
        //userData.Add("yearChild", yearChild.itemText.text);
        // Other Data
        //userData.Add("incPostal", incPostal.text);
        //userData.Add("Etnice", etniceName);
        return userData;
        //userData.Add()
    }

    private Dictionary<string, string> SetEtniceData()
    {
        Dictionary<string, string> dataEtnice = new Dictionary<string, string>();
        //dataEtnice.Add("incPostal", incPostal.text);
        dataEtnice.Add("Etnice", etniceName);
        
        dataEtnice.Add("Gender", gender);
        return dataEtnice;
    }



    public void GrantDay()
    {
        UpdateUserDataRequest request = new UpdateUserDataRequest();
        Dictionary<string, string> isUsed = new Dictionary<string, string>();
        isUsed.Add("lastLogin", currentDay);// Last Day Login
        isUsed.Add("isUsed", "false");
        isUsed.Add("amountUses", "0");
        request.Data = isUsed;
        request.PlayFabId = playFabId;
        
        PlayFabServerAPI.UpdateUserData(request, OnGrantDay, OnErrorUpdate);

    }
    private void OnGrantDay(UpdateUserDataResult result)
    {
        Debug.Log("Update Last Day");
        dayActivity.interactable = true;
        PlayFabOrder.instance.hasGrantDay = false;

    }


    public void GetDayUsed()
    {
        GetUserDataRequest request = new GetUserDataRequest();
        request.PlayFabId = playFabId;
        List<string> keyToSearch = new List<string>();
        keyToSearch.Add("isUsed");
        keyToSearch.Add("lastLogin");
        keyToSearch.Add("amountUses");
        keyToSearch.Add("Gender");
        keyToSearch.Add("parentName");
        keyToSearch.Add("childName");
        request.Keys = keyToSearch;
        PlayFabServerAPI.GetUserData(request, OnResultGetDay,OnErrorUpdate);
    }
    private void OnResultGetDay(GetUserDataResult result)
    {
        UserDataRecord dataResult;
        UserDataRecord lastLogin;
        UserDataRecord amountUses;
        UserDataRecord genderUser;
        UserDataRecord parentName;
        UserDataRecord childName;
        result.Data.TryGetValue("isUsed", out dataResult);
        result.Data.TryGetValue("lastLogin", out lastLogin);
        result.Data.TryGetValue("amountUses", out amountUses);
        result.Data.TryGetValue("Gender", out genderUser);
        result.Data.TryGetValue("parentName", out parentName);
        result.Data.TryGetValue("childName", out childName);

        if (parentName != null)
        {
            MenuManager.menuManager.parentFormName = parentName.Value;
            MenuManager.menuManager.childFormName = childName.Value;
        }
        
        if (dataResult == null)
        {
            GrantDay();
            PlayFabOrder.instance.hasGrantDay = true;
            
        }
        else
        {
            if (genderUser == null)
            {
                gender = "Male";
                playerAvatar.SetImagePlayer(gender);
            }
            else
            {
                gender = genderUser.Value;// Gender User
                playerAvatar.SetImagePlayer(gender);

            }
            
            Debug.Log(dataResult.Value);
            lastDayEnter = lastLogin.Value;
            MenuManager.menuManager.amountUses = Int32.Parse(amountUses.Value);
            if (dataResult.Value == "false")
            {
                dayActivity.interactable = true;
                extraBonus.interactable = false;
                isDayUsed = false;
                

            }

            int amountDays = MenuManager.menuManager.currentDay.amountDays;
            if ((dataResult.Value == "true") && (lastLogin.Value == currentDay) && (Int32.Parse(amountUses.Value) >= amountDays)) // Check if is CurrentDay
            {
                dayActivity.interactable = false;

                isDayUsed = true;
                if (scheduleManager.hasItem)// CHeck if Has Item to play Extra Game
                {

                    if (MenuManager.menuManager.usesItem == 2)
                    {
                        
                        dayActivity.interactable = false;
                        extraBonus.interactable = true;
                        MenuManager.menuManager.nameActivity = MenuManager.menuManager.bonusName;
                        MenuManager.menuManager.dayActivity = "All";

                    }

                    if (MenuManager.menuManager.usesItem == 1)
                    {
                        extraBonus.interactable = false;
                        dayActivity.interactable = false;
                        Debug.Log("No More Promos");
                      //  dayActivity.interactable = false;

                    }

                }
                else
                {
                    scheduleManager.GrantItemToPlayer();
                    dayActivity.interactable = false;
                    extraBonus.interactable = true;
                    MenuManager.menuManager.nameActivity = MenuManager.menuManager.bonusName;
                    MenuManager.menuManager.dayActivity = "All";

                }
                //

            }

            if (lastLogin.Value != currentDay)
            {
                GrantDay();
                PlayFabOrder.instance.hasGrantDay = true;
            }




        }

        var menu = MenuManager.menuManager;
        amountUsesText.text = menu.amountUses.ToString() + "/" + menu.currentDay.amountDays.ToString();
        PlayFabOrder.instance.isGetDayUsed = true;      
               
    }
    




    private void OnErrorGetDay(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());

        GrantDay();
    }




    public void UpdatePlayerGender(string gender)
    {
        int value = 1;
        if (gender == "Male")
        {
            value = 1;
        }
        if (gender == "Female")
        {
            value = 2;
        }


        UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest();
        request.PlayFabId = playFabId;
        List<StatisticUpdate> statsList = new List<StatisticUpdate>();
        StatisticUpdate stat = new StatisticUpdate();
        stat.StatisticName = "Gender";
        stat.Value = value;
        statsList.Add(stat);
        
        request.Statistics = statsList;
        PlayFabServerAPI.UpdatePlayerStatistics(request, result => { }, error => { });




    }


    public void GetPlayerStatistics()
    {
        GetPlayerStatisticsRequest request = new GetPlayerStatisticsRequest();
        request.PlayFabId = MenuManager.menuManager.playFabIdUser;
        List<string> names = new List<string>() { "Wins", "Gold" };

        request.StatisticNames = names;

        PlayFabServerAPI.GetPlayerStatistics(request, OnGetStatsResult, error => { Debug.Log(error.GenerateErrorReport()); });

    }

    private void OnGetStatsResult(GetPlayerStatisticsResult result)
    {
        foreach (StatisticValue stat in result.Statistics)
        {
            if (stat.StatisticName == "Wins")
            {
                amountWins.text = stat.Value.ToString();
            }
            if (stat.StatisticName == "Gold")
            {
                coinsPlayer.text = stat.Value.ToString();
            }

        }

        PlayFabOrder.instance.isGetStats = true;

    }
    





}// ENd Script
