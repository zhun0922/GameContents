using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using MySql.Data.MySqlClient;
using MySql.Data;

using System;
using System.Data;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;

public class LoginSignupMariaDBManager : MonoBehaviour
{
    //private MySqlConnection connection;
    private MySqlConnection myConnection = null;
    private MySqlCommand myCommand = null;
    private MySqlDataReader myDataReader = null;

    public TMP_InputField inputUserID;
    public TMP_InputField inputPassword;
    public TMP_InputField inputEmail;
    public TMP_Text displayMessage;

    private string username;
    private string password;
    private string email;

    //---------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        SetupSQLConnection();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //--------------------------
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

    //-------------------------- SQLConnect
    private void SetupSQLConnection()
    {
        if (myConnection == null)
        {
            string connectionString =
                "SERVER=61.245.246.242; Port=3308; " +
                "DATABASE=metaverse; " +
                "UID=wonkwangdc; " +
                "PASSWORD=wkdc2017;";
            try
            {
                myConnection = new MySqlConnection(connectionString);
                //connection.Open();
                Debug.Log("[MariaDB Connection] Connected successfully ..");
                displayMessage.text = "[MariaDB Connection] Connected successfully ..";
            }
            catch (MySqlException ex)
            {
                Debug.LogError("[MariaDB Connection Error] " + ex.ToString());
                displayMessage.text = "[MariaDB Connection Error] " + ex.ToString();
            }
        }
    }


    private bool CheckDataForMariaDB(int p)
    {
        if (p == 1) // Signup 일 때 
        {
            Debug.Log("Signup [" + username + " " + password + " " + email + "]");
            if ((username != null) && (password != null) && (email != null))
            {
                return true;
            }
            else
            {
                Debug.Log("[INPUT ERROR] User input has an ERROR (NOT Enough to signup)");
                displayMessage.text = "[INPUT ERROR] User input has an ERROR (NOT Enough to signup)";
                return false;
            }
        }
        else if (p == 2) // Login  일 때
        {
            Debug.Log("Login [" + username + " " + password + "]");
            if ((username != null) && (password != null))
            {
                return true;
            }
            else
            {
                Debug.Log("[INPUT ERROR] User login input has an ERROR (NOT Enough to login)");
                displayMessage.text = "[INPUT ERROR] User login input has an ERROR (NOT Enough to login)";
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    // --------------------------------- SINGUP
    public void RegisterMariaDB()
    {
        if (CheckDataForMariaDB(1))
        {
            Debug.Log("Signup is continued ..");
            //RegisterUserToMariaDBPlayer();
            if (RegisterUserToMariaDBPlayer())
            {
                inputUserID.text = "";
                inputPassword.text = "";
                inputEmail.text = "";

                displayMessage.text = "Ok, 정상적으로 회원가입되었습니다.";
                Debug.Log("Ok, 정상적으로 회원가입되었습니다.");
            }
        }
        else
        {
            displayMessage.text = "[ERROR] Error is occurred during your register ..";
            Debug.LogError("[ERROR] Error is occurred during your register ..");
        }
    }

    private bool RegisterUserToMariaDBPlayer()
    {
        string currentDateTimeValue = "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
        string usernameValue = "'" + username + "'";
        string passwordValue = "'" + password + "'";
        string emailValue = "'" + email + "'";

        //string commandText = string.Format("INSERT INTO daily_log (username, user_id) VALUES ({0}, {1})", "'megaplayer'", 10);
        string commandText = string.Format(
            "INSERT INTO player " +
            "(username, password, email, confirmationdate) " +
            "VALUES ({0}, {1}, {2}, {3})",
            usernameValue, passwordValue, emailValue, currentDateTimeValue
        );

        Debug.Log(commandText);

        if (myConnection != null)
        {
            //MySqlCommand command = connection.CreateCommand();
            //command.CommandText = commandText;
            myCommand = myConnection.CreateCommand();
            myCommand.CommandText = commandText;
            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                displayMessage.text = "[MariaDB SQL Error] " + ex.ToString();
                Debug.LogError("[MariaDB SQL Error] " + ex.ToString());
                return false;
            }
        }
        else
        {
            displayMessage.text = "[MariaDB Error] Connection error ..";
            Debug.LogError("[MariaDB Error] Connection error ..");
            return false;
        }
    }

    //----------------------------- LOGIN
    public void LoginMariaDB()
    {
        if (CheckDataForMariaDB(2))
        {
            Debug.Log("Login is continued ..");
            int res = LoginToMariaDBPlayer(); //playerID반환 유저가 아무도 없다면 -1 반환 
            if (res > 0) 
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().myGlobalPlayFabId = res.ToString();
                LoginToMariaDBLoginHistory(res);
            }
            else
            {
                displayMessage.text = "[" + res + "] Login failed ...";
                Debug.Log("[" + res + "] Login failed ...");
                inputUserID.text = "";
                inputPassword.text = "";
            }
        }
        else
        {
            displayMessage.text = "[ERROR] Error is occurred during your login ..";
            Debug.LogError("[ERROR] Error is occurred during your login ..");
        }
    }

    //private DataTable LoginToMariaDBPlayer()
    private int LoginToMariaDBPlayer()
    {
        //DataTable dt = new DataTable();

        string commandTextSelect = string.Format(
            "SELECT * FROM player WHERE username='" + username + "' AND password='" + password + "'"
        );
        Debug.Log(commandTextSelect);

        myConnection.Open(); //데이터 오픈

        //MySqlDataAdapter adapter = new MySqlDataAdapter(commandTextSelect, myConnection);  //어댑터 만든 다음 
        //adapter.Fill(dt); //Fill시켜야 하는데, 요즘 리더에서 이 기능을 다 해주는 느낌이라 주석처리 
        myCommand = new MySqlCommand(commandTextSelect, myConnection);
        myDataReader = myCommand.ExecuteReader();

        int playerID = -1;  //데이터가 없는것
        while (myDataReader.Read()) //데이터가 여러개있기 때문에 반복해서 접근 
        {
            Debug.Log(myDataReader[0] + "-" + myDataReader[1] + "-" + myDataReader[3] + "-" + myDataReader[6]);
            playerID = (int)myDataReader[0]; //ID(key)는 하나씩 증가, 14번째 회원가입 한 유저는 ID 14. 
        }

        myConnection.Close();

        //return dt;
        return playerID;
    }

    private void LoginToMariaDBLoginHistory(int id)
    {
        //다른 테이블인 LoginHistory테이블에 데이터 넣어주는것 
        string currentDateTimeValue = "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
        string usernameValue = "'" + username + "'";

        string commandText = string.Format(
            "INSERT INTO login_history (player_id, login_time) " +
            "VALUES ({0}, {1})",
            id, currentDateTimeValue
        );

        Debug.Log(commandText);

        if (myConnection != null)
        {
            myCommand = myConnection.CreateCommand();
            myCommand.CommandText = commandText;
            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();

                StartGame();
            }
            catch (System.Exception ex)
            {
                displayMessage.text = "[MariaDB SQL Error] " + ex.ToString();
                Debug.LogError("[MariaDB SQL Error] " + ex.ToString());
            }
        }
        else
        {
            displayMessage.text = "[MariaDB Error] Connection error ..";
            Debug.LogError("[MariaDB Error] Connection error ..");
        }
    }

    //------------------- Game Start !
    void StartGame()
    {
        displayMessage.text = "Now, start the game, enjoy it";
        Debug.Log("Now, start the game, enjoy it");
    }
}
