using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{
    public GameObject imageMen;
    public GameObject imageGirl;

    public void SetImagePlayer(string gender)
    {
        if (gender == "Female")
        {
            imageGirl.SetActive(true);
        }
        if (gender == "Male")
        {
            imageMen.SetActive(true);
        }

    }


}
