using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class LoadGameLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (ActiveData.PlayerData != null)
        {

        }
        else
        {
            print("Currently playing in offline mode. Data will not be saved.");
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
