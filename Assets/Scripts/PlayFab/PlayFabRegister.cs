using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System.IO;




public class PlayFabRegister : MonoBehaviour
{
    public string titleId;
    public InputField userNameInput;
    public InputField passwordInput;
    public InputField confirmPassword;
    public InputField emailInput;
    private string userName;
    private string passwordText;
    private string emailText;
    //public InputField.OnValidateInput onValidate;
    public Text textError;
    public GameObject panelError;
    public TextMeshProUGUI amountCoins;
    public TextMeshProUGUI namePlayer;
    public GameObject registerPanel;
    public GameObject loginPanel;
    public GameObject formPanel;
    public GameObject menuPanel;
    private bool firstLogin = false;
    public ScheduleManager scheduleManager;
    public string parentName;
    public string childName;
    private bool hasForm;


    // Start is called before the first frame update
    private void Awake()
    {
        //LoginUser();
    }
    private void Start()
    {
        //PlayFabManager.instance.GetCurrentDay();


    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    public void LoginUser()
    {
        if ((PlayerPrefs.HasKey("UserName")) && (PlayerPrefs.HasKey("Password")))
        {
            registerPanel.SetActive(false);
            LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
            request.Username = PlayerPrefs.GetString("UserName");
            userName = PlayerPrefs.GetString("UserName");
            MenuManager.menuManager.playerName = userName;
            request.Password = PlayerPrefs.GetString("Password");
            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginResult, OnLoginError);
            
            

        }
        else
        {
            
            registerPanel.SetActive(true);
            PlayFabOrder.instance.isRegister = true;
        }
    }
    private void OnLoginResult(LoginResult result)
    {
        Debug.Log("Login Succes");
        if (!firstLogin)
        {
            menuPanel.SetActive(true);
            registerPanel.SetActive(false);
        }
       
        // Set Login to True
        namePlayer.text = userName;
        //var dataPlayfab = result.InfoResultPayload;// Get Currency Data
        //int currencyPlayer;
        PlayFabManager.instance.playFabId = result.PlayFabId;
        MenuManager.menuManager.playFabIdUser = result.PlayFabId;
        //PlayFabCurrency.playFabCurrency.GetCurrency();
        //scheduleManager.GetItem();
        PlayFabOrder.instance.isLogin = true;
        //PlayFabOrder.instance.isRegister = false;
        // Get Day 


    }
    private void OnLoginError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        Debug.Log(error.ErrorMessage);
        Debug.Log(error.Error.ToString());
    }


    public void RegisterUser()
    {
        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
        request.TitleId = titleId;
        request.Username = userNameInput.text;
        request.DisplayName = userNameInput.text;
        request.Password = passwordInput.text;
        request.Email = emailInput.text;
        
        userName = userNameInput.text;// Set Variable Names And Password
        passwordText = passwordInput.text;
        emailText = emailInput.text;
        firstLogin = true;


        //request.RequireBothUsernameAndEmail = false;
        PlayFabClientAPI.RegisterPlayFabUser(request, SuccesRegister, ErrorLogin);

        
                
    }
    private void SuccesRegister(RegisterPlayFabUserResult result)
    {
        
        Debug.Log("Player Register");
        PlayerPrefs.SetString("UserName", userName);// Set PlayerPrefs User and Password
        PlayerPrefs.SetString("Password", passwordText);
        PlayerPrefs.SetString("Email", emailText);
        //PlayerPrefs.SetString("Email"), );
        PlayFabManager.instance.playFabId = result.PlayFabId;
        MenuManager.menuManager.playFabIdUser = result.PlayFabId;
        registerPanel.SetActive(false);
        formPanel.SetActive(true);
        LoginUser();// Login User After Register
    }

    private void ErrorLogin(PlayFabError error)
    {
        var newError = error.ErrorDetails;
        foreach (var myKey in newError)
            {
            Debug.Log(myKey.Key);
             myKey.Value.ForEach(detail => { textError.text += detail;}); 

            }
        panelError.SetActive(true);


    }
    
    public void OnClickLogin()
    {
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
        request.Username = userNameInput.text;
        userName = userNameInput.text;
        request.Password = passwordInput.text;
        passwordText = passwordInput.text;
        
        firstLogin = false;
        hasForm = true;
        PlayFabClientAPI.LoginWithPlayFab(request, OnResultClick, OnErrorLogin);        
    }

    private void OnResultClick(LoginResult result)
    {

        PlayFabOrder.instance.isLogin = true;
        PlayFabOrder.instance.isRegister = false;
        namePlayer.text = userName;
              
        PlayFabManager.instance.playFabId = result.PlayFabId;
        MenuManager.menuManager.playFabIdUser = result.PlayFabId;
        PlayerPrefs.SetString("UserName", userName);// Set PlayerPrefs User and Password
        PlayerPrefs.SetString("Password", passwordText);
        registerPanel.SetActive(false);
        menuPanel.SetActive(true);
        







    }

   
    private void OnErrorLogin(PlayFabError error)
    {
        panelError.SetActive(true);
        textError.text = "Wrong UserName or Password";
        Debug.Log("Wrong UserName or Password");

    }


      

    


}
