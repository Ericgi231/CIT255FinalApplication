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
            CashTracker.Money = ActiveData.PlayerData.FarmData[0].Score;

            foreach (GameObject soil in GameObject.FindGameObjectsWithTag("Dirt"))
            {
                SoilLogic props = soil.GetComponent<SoilLogic>();
                props.CreateCrop();

                foreach (SoilDataObject data in ActiveData.PlayerData.SoilData)
                {
                    if (props.CropId == data.LandId)
                    {
                        props.IsTilled = data.IsTilled;
                        if (data.GrowTime > 0)
                        {
                            props.CurrentCrop = new Crop()
                            {
                                GrowTime = data.GrowTime,
                                Age = data.Age,
                                Value = data.Value
                            };

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

                            float size = 2 + (props.CurrentCrop.Age / props.CurrentCrop.GrowTime);
                            Vector3 v = new Vector3(size,size,size);
                            props.CropObject.transform.localScale = v;
                        }
                    }
                }

                if (props.IsTilled)
                {
                    props.SoilTilled();
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
