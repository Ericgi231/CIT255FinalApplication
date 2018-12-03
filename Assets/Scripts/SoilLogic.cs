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

    private bool IsWattered;
    private bool IsTilled;

    private Crop CurrentCrop;
    private GameObject CropObject;

	// Use this for initialization
	void Start () {

        IsTilled = false;
        IsWattered = false;

        CurrentCrop = null;

        if (WeatherController.IsRaining == true)
        {
            IsWattered = true;
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (CurrentCrop != null)
        {
            CropObject.transform.localScale += new Vector3(0.1f,0.1f,0.1f);
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
                    IsTilled = true;
                    GetComponent<MeshRenderer>().material = tilled;
                }
                break;
            case Tool.WATTERCAN:
                IsWattered = true;
                GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case Tool.TRIMMER:
                break;
            case Tool.SHOVEL:
                CurrentCrop = null;
                Destroy(CropObject);
                CropObject = null;

                IsTilled = false;
                GetComponent<MeshRenderer>().material = raw;
                break;
            case Tool.CARROT:
                if (IsTilled && CurrentCrop == null)
                {
                    CurrentCrop = new Crop()
                    {
                        GrowTime = 30,
                        Age = 0,
                        Value = 10,
                        Material = CarrotMaterial,
                        Mesh = CarrotMesh
                    };

                    CreateCrop();

                    CropObject.GetComponent<MeshFilter>().mesh = CarrotMesh;
                    CropObject.GetComponent<MeshRenderer>().material = CarrotMaterial;
                }
                break;
            case Tool.PUMPKIN:
                if (IsTilled && CurrentCrop == null)
                {
                    CurrentCrop = new Crop()
                    {
                        GrowTime = 120,
                        Age = 0,
                        Value = 10,
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
                        GrowTime = 60,
                        Age = 0,
                        Value = 10,
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
}
