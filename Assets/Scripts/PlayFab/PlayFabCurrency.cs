using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ServerModels;
using TMPro;

public class PlayFabCurrency : MonoBehaviour
{
    public static PlayFabCurrency playFabCurrency;
    public int amountCoins;
    public TextMeshProUGUI amountCurrency;
    

    // Start is called before the first frame update

    private void Awake()
    {
        
        if (playFabCurrency == null)
        {
            playFabCurrency = this;
        }
      
    }
    void Start()
    {
         //GetCurrency();
    }
      

    public void GetCurrency()
    {

        GetPlayerCombinedInfoRequest request = new GetPlayerCombinedInfoRequest();
        request.PlayFabId = PlayFabManager.instance.playFabId;
        GetPlayerCombinedInfoRequestParams param = new GetPlayerCombinedInfoRequestParams();
        param.GetUserVirtualCurrency = true;// Set To Get the Currency
        request.InfoRequestParameters = param;
        PlayFabServerAPI.GetPlayerCombinedInfo(request, OnResultCurrency, error => { Debug.Log("Error"); });
        
           
        //request.
        //PlayFabSer
    }

    private void OnResultCurrency(GetPlayerCombinedInfoResult result)
    {
        amountCoins = result.InfoResultPayload.UserVirtualCurrency["US"];// Set Amount Coins
        PlayFabManager.instance.coinsPlayer.text = amountCoins.ToString();
        

    }

    public void AddCurreny(int amount)
    {
        AddUserVirtualCurrencyRequest request = new AddUserVirtualCurrencyRequest();
        request.PlayFabId = PlayFabManager.instance.playFabId;
        request.Amount = amount;
        request.VirtualCurrency = "US";
        PlayFabServerAPI.AddUserVirtualCurrency(request, OnAddCurrencySucces, OnErrorCurrency);
        amountCoins += amount;
        PlayFabManager.instance.coinsPlayer.text = (amountCoins + amount).ToString();

    }

    private void OnAddCurrencySucces(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Succes Add Currency");
        
    }
   private void OnErrorCurrency(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
