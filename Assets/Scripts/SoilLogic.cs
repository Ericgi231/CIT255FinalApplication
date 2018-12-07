using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class SoilLogic : MonoBehaviour {

    public Material raw;
    public Material tilled;

    public Mesh CarrotMesh;
    public Mesh PumpkinMesh;
    public Mesh LettuceMesh;

    public Material CarrotMaterial;
    public Material PumpkinMaterial;
    public Material LettuceMaterial;

    public bool IsWattered;
    public bool IsTilled;

    public Crop CurrentCrop;
    public GameObject CropObject;

    public int CropId;

	// Use this for initialization
	void Start () {

        IsTilled = false;
        IsWattered = false;

        CurrentCrop = null;

        if (WeatherController.IsRaining)
        {
            IsWattered = true;
            GetComponent<MeshRenderer>().material.color = new Color32(200, 200, 250, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (CurrentCrop != null && CurrentCrop.Age < CurrentCrop.GrowTime)
        {
            float GrowRate;

            if (IsWattered)
            {
                CurrentCrop.Age += 2;
                GrowRate = (float)(4 / CurrentCrop.GrowTime);
            }
            else
            {
                CurrentCrop.Age++;
                GrowRate = (float)(2 / CurrentCrop.GrowTime);
            }
            
            Vector3 GrowVector = new Vector3(GrowRate,GrowRate,GrowRate);

            CropObject.transform.localScale += GrowVector;
        }
	}

    void OnMouseDown()
    {
        SetState();
    }

    void SetState()
    {
        switch (PlayerController.CurrentTool)
        {
            case Tool.HOE:
                if (!IsTilled)
                {
                    SoilTilled();
                }
                break;
            case Tool.WATTERCAN:
                IsWattered = true;
                GetComponent<MeshRenderer>().material.color = new Color32(200,200,250,0);
                break;
            case Tool.TRIMMER:
                if (CurrentCrop.Age >= CurrentCrop.GrowTime)
                {
                    CashTracker.Money += CurrentCrop.Value;

                    SoilDestroyed();

                    if (WeatherController.IsRaining)
                    {
                        IsWattered = true;
                    }
                }
                break;
            case Tool.SHOVEL:
                SoilDestroyed();
                break;
            case Tool.CARROT:
                if (IsTilled && CurrentCrop == null)
                {
                    CurrentCrop = new Crop()
                    {
                        GrowTime = 4320,
                        Age = 0,
                        Value = 10,
                        Material = CarrotMaterial,
                        Mesh = CarrotMesh
                    };

                    CreateCrop();

                    CropObject.GetComponent<MeshFilter>().mesh = CarrotMesh;
                    CropObject.GetComponent<MeshRenderer>().material = CarrotMaterial;
                    CropObject.transform.position += new Vector3(0.0f, -0.2f, 0.0f);
                }
                break;
            case Tool.PUMPKIN:
                if (IsTilled && CurrentCrop == null)
                {
                    CurrentCrop = new Crop()
                    {
                        GrowTime = 17280,
                        Age = 0,
                        Value = 90,
                        Material = PumpkinMaterial,
                        Mesh = PumpkinMesh
                    };

                    CreateCrop();

                    CropObject.GetComponent<MeshFilter>().mesh = PumpkinMesh;
                    CropObject.GetComponent<MeshRenderer>().material = PumpkinMaterial;
                }
                break;
            case Tool.LETTUCE:
                if (IsTilled && CurrentCrop == null)
                {
                    CurrentCrop = new Crop()
                    {
                        GrowTime = 8640,
                        Age = 0,
                        Value = 30,
                        Material = LettuceMaterial,
                        Mesh = LettuceMesh
                    };

                    CreateCrop();

                    CropObject.GetComponent<MeshFilter>().mesh = LettuceMesh;
                    CropObject.GetComponent<MeshRenderer>().material = LettuceMaterial;
                }
                break;
            default:
                break;
        }
    }

    void CreateCrop()
    {
        CropObject = new GameObject("Crop");
        CropObject.transform.parent = this.transform;
        CropObject.AddComponent<MeshFilter>();
        CropObject.AddComponent<MeshRenderer>();

        CropObject.transform.position = transform.position;
        CropObject.transform.position += new Vector3(0.0f,0.2f,0.0f);
    }

    void SoilTilled()
    {
        IsTilled = true;
        GetComponent<MeshRenderer>().material = tilled;
        if (IsWattered)
        {
            GetComponent<MeshRenderer>().material.color = new Color32(200, 200, 250, 0);
        }
    }

    void SoilDestroyed()
    {
        CurrentCrop = null;
        Destroy(CropObject);
        CropObject = null;

        IsTilled = false;
        GetComponent<MeshRenderer>().material = raw;
        if (IsWattered)
        {
            GetComponent<MeshRenderer>().material.color = new Color32(200, 200, 250, 0);
        }
    }
}
