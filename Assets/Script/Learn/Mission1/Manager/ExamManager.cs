using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamManager : MonoBehaviour
{
    private MySqlConnection myConnection = null;
    private MySqlCommand myCommand = null;

    [SerializeField] Player player;
    public static ExamManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        SetupSQLConnection();
    }
    private void SetupSQLConnection()
    {
        if (myConnection == null)
        {
            string connectionString =
                "SERVER=localhost; Port=3308; " +
                "DATABASE=gamecontent; " +
                "UID=root; " +
                "PASSWORD=dawson121;" +
                "CharSet=utf8";
            try
            {
                myConnection = new MySqlConnection(connectionString);
                //connection.Open();
                Debug.Log("[MariaDB Connection] Connected successfully ..");
                //displayMessage.text = "[MariaDB Connection] Connected successfully ..";
            }
            catch (MySqlException ex)
            {
                Debug.LogError("[MariaDB Connection Error] " + ex.ToString());
                //displayMessage.text = "[MariaDB Connection Error] " + ex.ToString();
            }
        }
    }

    public bool UpdateWpointsToMariaDBPlayer()
    {
        string wpoints = "'" + player.Point + "'";

        //string commandText = string.Format("INSERT INTO daily_log (username, user_id) VALUES ({0}, {1})", "'megaplayer'", 10);
        string commandText = string.Format(
           "UPDATE player SET wpoints = {0} WHERE username = '1234'",wpoints //원래 이 username을 로그인하면서 받아와야하는데 그게안됨.. 임의로 1234
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
                //displayMessage.text = "[MariaDB SQL Error] " + ex.ToString();
                Debug.LogError("[MariaDB SQL Error] " + ex.ToString());
                return false;
            }
        }
        else
        {
            //displayMessage.text = "[MariaDB Error] Connection error ..";
            Debug.LogError("[MariaDB Error] Connection error ..");
            return false;
        }
    }
}
