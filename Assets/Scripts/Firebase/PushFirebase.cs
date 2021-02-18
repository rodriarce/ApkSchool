using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;

public class PushFirebase : MonoBehaviour
{
    // Start is called before the first frame update
    public string pushToken;
   
    public void SetFirebase()
    {
        //Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        //Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //var app = Firebase.FirebaseApp.DefaultInstance;
                Debug.Log("Firebase Ready");
                SetFirebaseMessage();
                PlayFabOrder.instance.isFirebaseLoad = true;
                SetNotfication();
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
    
    private void SetNotfication()
    {
        AndroidDevicePushNotificationRegistrationRequest request = new AndroidDevicePushNotificationRegistrationRequest();
        request.DeviceToken = pushToken;
        request.SendPushNotificationConfirmation = true;
        request.ConfirmationMessage = "Succes Push";
        PlayFabClientAPI.AndroidDevicePushNotificationRegistration(request, OnResultNotification, error => { Debug.Log(error.GenerateErrorReport()); });
    }

    private void OnResultNotification(AndroidDevicePushNotificationRegistrationResult result)
    {
        Debug.Log("Succes add Push Notification");

    }

    private void SetFirebaseMessage()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
    }


    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);
        pushToken = token.Token;
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
       Debug.Log("Received a new message from: " + e.Message.From);
    }



}
