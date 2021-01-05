using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOrder : MonoBehaviour
{
    public PlayFabPlayer playFabPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        playFabPlayer = GetComponent<PlayFabPlayer>();
        
    }



   

}
