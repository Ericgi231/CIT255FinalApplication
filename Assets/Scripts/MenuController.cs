using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DataLayer;
using Assets.Scripts.Models;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private DataContainer DataContainer;
    private IRepository DataService;
    private int ActiveUserId;

    public Button Play, PlayOffline;
    public InputField Name, Pass;
    public Text Error;

	// Use this for initialization
	void Start () {
        Play.onClick.AddListener(CheckAccount);
        PlayOffline.onClick.AddListener(StartGame);

        DataService = new MySqlDataService();

        ActiveUserId = -1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CheckAccount()
    {
        if (Name.text == "")
        {
            Error.text = "Please Enter Name";
            return;
        }
        
        if (Pass.text == "")
        {
            Error.text = "Please Enter Pass";
            return;
        }

        Error.text = "";

        try
        {
            DataContainer = DataService.ReadAll();
        }
        catch (System.Exception)
        {
            Error.text = "Connection Failed";
        }

        foreach (FarmDataObject user in DataContainer.FarmData)
        {
            if (Name.text == user.UserName)
            {
                if (Pass.text == user.Pass)
                {
                    ActiveUserId = user.UserId;
                }
                else
                {
                    ActiveUserId = -999;
                }
            }
        }

        if (ActiveUserId >= 0)
        {
            StartGame();
        }
        else if (ActiveUserId == -999)
        {
            Error.text = "Password Failed";
        }
        else
        {
            CreateUser();
            Error.text = "New User Created";
        }

    }

    void StartGame()
    {
        try
        {
            ActiveData.PlayerData = DataService.ReadById(ActiveUserId);
            SceneManager.LoadScene("Main",LoadSceneMode.Single);
        }
        catch (System.Exception)
        {
            Error.text = "Failed to get data";
        }
    }

    void CreateUser()
    {
        DataService.WriteAll(new DataContainer() { });
    }
}
