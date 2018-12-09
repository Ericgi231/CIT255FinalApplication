using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;
using Assets.Scripts.DataLayer;
using System;

public class LoadGameLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (ActiveData.PlayerData != null)
        {
            //set points
            CashTracker.Money = ActiveData.PlayerData.FarmData[0].Score;

            //loop through each soil on the map
            foreach (GameObject soil in GameObject.FindGameObjectsWithTag("Dirt"))
            {
                //assign props of soil to variable
                SoilLogic props = soil.GetComponent<SoilLogic>();

                //loop though each soil in the save file
                foreach (SoilDataObject data in ActiveData.PlayerData.SoilData)
                {
                    //if selected soil equal selected soil in data
                    if (props.CropId == data.LandId)
                    {
                        //check if soil is tilled
                        props.IsTilled = data.IsTilled;

                        if (props.IsTilled)
                        {
                            props.SoilTilled();
                        }

                        // if crop is present in soil
                        if (data.GrowTime > 0)
                        {
                            //create physical crop
                            props.CreateCrop();

                            //set props of crop
                            props.CurrentCrop = new Crop()
                            {
                                GrowTime = data.GrowTime,
                                Age = data.Age,
                                Value = data.Value
                            };

                            //assign looks to pysical crop
                            switch (data.Material)
                            {
                                case "carrot":
                                    props.CurrentCrop.Material = props.CarrotMaterial;
                                    props.CurrentCrop.Mesh = props.CarrotMesh;

                                    props.CropObject.GetComponent<MeshRenderer>().material = props.CarrotMaterial;
                                    props.CropObject.GetComponent<MeshFilter>().mesh = props.CarrotMesh;
                                    break;
                                case "pumpkin":
                                    props.CurrentCrop.Material = props.PumpkinMaterial;
                                    props.CurrentCrop.Mesh = props.PumpkinMesh;

                                    props.CropObject.GetComponent<MeshRenderer>().material = props.PumpkinMaterial;
                                    props.CropObject.GetComponent<MeshFilter>().mesh = props.PumpkinMesh;
                                    break;
                                case "lettuce":
                                    props.CurrentCrop.Material = props.LettuceMaterial;
                                    props.CurrentCrop.Mesh = props.LettuceMesh;

                                    props.CropObject.GetComponent<MeshRenderer>().material = props.LettuceMaterial;
                                    props.CropObject.GetComponent<MeshFilter>().mesh = props.LettuceMesh;
                                    break;
                                default:
                                    break;
                            }

                            TimeSpan timeOff = DateTime.Now - Convert.ToDateTime(ActiveData.PlayerData.FarmData[0].LastSave);

                            props.CurrentCrop.Age = props.CurrentCrop.Age + ((int)timeOff.TotalSeconds * 6);
                            if (props.CurrentCrop.Age > props.CurrentCrop.GrowTime)
                            {
                                props.CurrentCrop.Age = props.CurrentCrop.GrowTime;
                            }

                            float size = 2 + (props.CurrentCrop.Age / props.CurrentCrop.GrowTime);
                            Vector3 v = new Vector3(size,size,size);
                            props.CropObject.transform.localScale = v;
                        }
                    }
                }
            }
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
