using System;
using AppsFlyerSDK;
using States;
using UnityEngine;
using VContainer;

public class ProcessDeepLinkMngr : MonoBehaviour
{
    public static ProcessDeepLinkMngr Instance { get; private set; }
    public string deeplinkURL;

    [Inject] private MenuState _menuState; 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;                
            AppsFlyer.OnDeepLinkReceived += AppsFlyerOnDeepLinkReceived;
            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                // Cold start and Application.absoluteURL not null so process Deep Link.
                onDeepLinkActivated(Application.absoluteURL);
            }
            // Initialize DeepLink Manager global variable.
            else deeplinkURL = "[none]";
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void AppsFlyerOnDeepLinkReceived(object sender, EventArgs e)
    {
    }

    private void onDeepLinkActivated(string url)
    {
        // Update DeepLink Manager global variable, so URL can be accessed from anywhere.
        deeplinkURL = url;
        
// Decode the URL to determine action. 
// In this example, the application expects a link formatted like this:
// unitydl://mylink
// unitydl://mylink?start=true
// unitydl://mylink?policy
        string param = url.Split('?')[1];
        // bool validScene;
        switch (param)
        {
            case "start=true":
                _menuState.StartGame();
                break;
            case "policy":
                _menuState.ShowPolicy();
                break;
        }
        // if (validScene) SceneManager.LoadScene(param);
    }
}