using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.DataLayer;
using Assets.Scripts.Models;
using System;

public class SaveLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        SoilDataObjectContainer dirts = new SoilDataObjectContainer();
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Dirt"))
        {
            SoilLogic props = gameObject.GetComponent<SoilLogic>();
            SoilDataObject soilData = new SoilDataObject()
            {
                OwnerId = ActiveData.PlayerData.FarmData[0].UserId,
                LandId = props.CropId,
                IsTilled = props.IsTilled,
                GrowTime = props.CurrentCrop.GrowTime,
                Age = props.CurrentCrop.Age,
                Value = props.CurrentCrop.Value,
                Material = props.CurrentCrop.Material.ToString(),
                Mesh = props.CurrentCrop.Material.ToString()
            };
            dirts.Add(soilData);
        }

        ActiveData.PlayerData.FarmData[0].LastSave = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        ActiveData.PlayerData.FarmData[0].Score = (int)CashTracker.Money;

        DataContainer data = new DataContainer()
        {
            FarmData = ActiveData.PlayerData.FarmData,
            SoilData = dirts
        };

        IRepository dataLogic = new MySqlDataService();
        dataLogic.DeleteById(ActiveData.PlayerData.FarmData[0].UserId);
        dataLogic.WriteAll(data);
    }
}
