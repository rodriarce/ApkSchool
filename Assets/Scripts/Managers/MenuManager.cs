using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.AdminModels;

public class MenuManager : MonoBehaviour
{
    // Colors: E76C47 BLue: 475279
    public static MenuManager menuManager;
    public string nameActivity;// Name of The Activity to set in The Game
    public string dayActivity;
    public string playFabIdUser;
    public string childName;
    public string parentName;
    public int amountUses;
    public Dictionary<string, string> daysActivity;
    public DaysProperties currentDay;
    public int? usesItem;
    public string itemUseId;
    public string bonusName;
    public string playerName;
    public string childFormName;
    public string parentFormName;
    public bool isFinishLevel;
    

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
       if (menuManager == null)
        {
            menuManager = this;
        }
       else
        {
            Destroy(this.gameObject);
        }
               
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetDayActivity()
    {
        if ((dayActivity == "Saturday" || dayActivity == "Sunday"))
        {
            dayActivity = "Weekend";
        }
        Debug.Log(daysActivity["Weekend"]);
        nameActivity = daysActivity[dayActivity];
    }

   

    public void OnActionScene()
    {
              
        
        SceneManager.LoadScene("ActionScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
