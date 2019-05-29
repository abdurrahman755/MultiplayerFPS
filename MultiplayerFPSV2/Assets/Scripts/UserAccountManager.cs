using UnityEngine;
using System.Collections;
using DatabaseControl;
using UnityEngine.SceneManagement;

public class UserAccountManager : MonoBehaviour {

    public static UserAccountManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    public static string LoggedIn_Username { get; protected set; } //stores username once logged in
    private static string LoggedIn_Password = ""; //stores password once logged in

    public static bool IsLoggedIn { get; protected set; }

    public string loggedInSceneName = "Lobby";
    public string loggedOutSceneName = "LoginMenu";

    public delegate void OnDataReceivedCallBack(string data);

    public void LogOut()
    {
        LoggedIn_Username = "";
        LoggedIn_Password = "";

        IsLoggedIn = false;

        //Debug.Log("log out");

        SceneManager.LoadScene(loggedOutSceneName);
    }

    public void LogIn(string username, string password)
    {
        LoggedIn_Username = username;
        LoggedIn_Password = password;

        IsLoggedIn = true;

        //Debug.Log("logged in as: " + username);

        SceneManager.LoadScene(loggedInSceneName);
    }

    public void SendData(string data)
    { //called when the 'Send Data' button on the data part is pressed
        if (IsLoggedIn)
        {
            //ready to send request
            StartCoroutine(sendSendDataRequest(LoggedIn_Username, LoggedIn_Password, data)); //calls function to send: send data request
        }
    }

    IEnumerator sendSendDataRequest(string username, string password, string data)
    {
        IEnumerator eee = DatabaseControl.DCF.SetUserData(username, password, data);
        while (eee.MoveNext())
        {
            yield return eee.Current;
        }
        string response = eee.Current as string;
        if (response == "ContainsUnsupportedSymbol")
        {
            //One of the parameters contained a - symbol
            Debug.Log("Data Upload Error. Could be a server error. To check try again, if problem still occurs, contact us.");
        }
        if (response == "Error")
        {
            //Error occurred. For more information of the error, DatabaseControl.DCF.Login could
            //be used with the same username and password
            Debug.Log("Data Upload Error: Contains Unsupported Symbol '-'");
        }
    }

    public void GetData(OnDataReceivedCallBack onDataReceived)
    { //called when the 'Get Data' button on the data part is pressed

        if (IsLoggedIn)
        {
            //ready to send request
            StartCoroutine(sendGetDataRequest(LoggedIn_Username, LoggedIn_Password, onDataReceived)); //calls function to send get data request
        }
    }

    IEnumerator sendGetDataRequest(string username, string password, OnDataReceivedCallBack onDataReceived)
    {
        string data = "ERROR";

        IEnumerator eeee = DatabaseControl.DCF.GetUserData(username, password);
        while (eeee.MoveNext())
        {
            yield return eeee.Current;
        }
        string response = eeee.Current as string;
        if (response == "Error")
        {
            //Error occurred. For more information of the error, DC.Login could
            //be used with the same username and password
            Debug.Log("Data Upload Error. Could be a server error. To check try again, if problem still occurs, contact us.");
        }
        else
        {
            if (response == "ContainsUnsupportedSymbol")
            {
                //One of the parameters contained a - symbol
                Debug.Log("Get Data Error: Contains Unsupported Symbol '-'");
            }
            else
            {
                //Data received in returned.text variable
                string DataRecieved = response;
                data = DataRecieved;
            }
        }

        if(onDataReceived != null)
            onDataReceived.Invoke(data);
    }

}
