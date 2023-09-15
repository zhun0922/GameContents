using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using PlayFab;
using PlayFab.ClientModels;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI showButtonText;

    //[System.NonSerialized]
    public string myGlobalPlayFabId;

    [System.NonSerialized]
    public static GameManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TestButton()
    {

    }

    //------------- Getter

    public void GetUserData(string myPlayFabId)
    {
        if (myPlayFabId == null) { myPlayFabId = myGlobalPlayFabId; }

        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myPlayFabId,
            Keys = null
        },
        result => {
            Debug.Log("Got user data:");
            if (result.Data == null || !result.Data.ContainsKey("HighestScore")) { Debug.Log("NO Values"); }
            else { Debug.Log("HighestScore is " + result.Data["HighestScore"].Value); }
        },
        error => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void GetUserData1(string myPlayFabId)
    {
        if (myPlayFabId == null) { myPlayFabId = myGlobalPlayFabId; }

        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            PlayFabId = myPlayFabId,
            Keys = null
        },
        GetUserData1Success,
        GetUserData1Failure
        );
    }
    private void GetUserData1Success(GetUserDataResult result)
    {
        Debug.Log("Got user data:");
        if (result.Data == null || !result.Data.ContainsKey("HighestScore")) { Debug.Log("NO Value"); }
        else
        {
            Debug.Log("HighestScore is " + result.Data["HighestScore"].Value);
            Debug.Log("Address is " + result.Data["Address"].Value);
        }
    }
    private void GetUserData1Failure(PlayFabError error)
    {
        Debug.Log("Got error retrieving user data:");
        Debug.Log(error.GenerateErrorReport());
    }

    //---------------- Setter

    public void SetUserData()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
                {"Nationality","South Korea" }, {"University","WonKwang Univ." }
            }
        },
        result => Debug.Log("Successfully updated user data"),
        error => {
            Debug.Log("Got error setting user data");
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void SetUserData1()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
                {"Nationality","Korea" }, {"University","WonKwang" }
            }
        },
        SetUserData1Success,
        SetUserData1Failure
        );
    }

    private void SetUserData1Success(UpdateUserDataResult result)
    {
        Debug.Log("Successfully updated user data");
    }
    private void SetUserData1Failure(PlayFabError error)
    {
        Debug.Log("Got error setting user data");
        Debug.Log(error.GenerateErrorReport());
    }

    //----------------------- Get Readonly Data
    public void GetUserReadOnlyData()
    {
        PlayFabServerAPI.GetUserReadOnlyData(new PlayFab.ServerModels.GetUserDataRequest() { PlayFabId = myGlobalPlayFabId },
        result => {
            if (result.Data == null || !result.Data.ContainsKey("Sister")) Debug.Log("No Sister");
            else Debug.Log("Sister: " + result.Data["Sister"].Value);
        },
        error => {
            Debug.Log("Got error getting read-only user data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }
}
