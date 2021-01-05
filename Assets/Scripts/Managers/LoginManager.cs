using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        SetRotation();
    }


    private void SetRotation()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
