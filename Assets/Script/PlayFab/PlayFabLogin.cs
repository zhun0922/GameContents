using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab;
using PlayFab.ClientModels;
//using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayFabLogin: MonoBehaviour
{
    /*
    public InputField inputUserID;
    public InputField inputPassword;
    public InputField inputEmail;
    public Text ErrorText;
    */

    public TMP_InputField inputUserID;
    public TMP_InputField inputPassword;
    public TMP_InputField inputEmail;
    public TMP_Text displayMessage;

    private string username;
    private string password;
    private string email;

    private const string gameCont = "GameManager";

    // Start is called before the first frame update
    void Start()
    {
        PlayFabSettings.TitleId = "3465C";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //---------------------------------

    public void UsernameValueChanged()
    {
        username = inputUserID.text.ToString();
    }

    public void PasswordValueChanged()
    {
        password = inputPassword.text.ToString();
    }

    public void EmailValueChanged()
    {
        email = inputEmail.text.ToString();
    }

    // -------------- Login
    public void Login()
    {
        Debug.Log(username + "/" + password);
        var request = new LoginWithPlayFabRequest { Username = username, Password = password };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login successfully");
        displayMessage.text = "Login successfully [" + result.PlayFabId + "]";

        //GameObject.Find("GameManager").GetComponent<GameManager>().myGlobalPlayFabId = result.PlayFabId;
        GameManager.Instance.myGlobalPlayFabId = result.PlayFabId;
        StartGame();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Login FAILED");
        Debug.LogWarning(error.GenerateErrorReport());
        displayMessage.text = error.GenerateErrorReport();
    }

    //------------- Register (Signup)
    public void Register()
    {
        Debug.Log(username + "/" + password + "/" + email);
        var request = new RegisterPlayFabUserRequest { Username = username, Password = password, Email = email };
        PlayFabClientAPI.RegisterPlayFabUser(request, RegisterSuccess, RegisterFailure);
    }

    private void RegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Signup successfully");
        displayMessage.text = "Signup successfully";
    }

    private void RegisterFailure(PlayFabError error)
    {
        Debug.LogWarning("Signup FAILED");
        Debug.LogWarning(error.GenerateErrorReport());
        displayMessage.text = error.GenerateErrorReport();
    }

    void StartGame()
    {
        Debug.Log("Now, start the game, enjoy it");

        //GameObject.Find("GameManager").SendMessage("ShowLoginPanel", "true");
        GameObject.Find("GameManager").SendMessage("TestButton");
    }
    //---------------------------------
}
