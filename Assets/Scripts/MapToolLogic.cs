using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class MapToolLogic : MonoBehaviour {

    public Tool ToolType;
    public Mesh NewMesh;

    private GameObject Player;
    private GameObject HeldTool;

	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player");
        HeldTool = GameObject.Find("HeldTool");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        PlayerController.CurrentTool = ToolType;
        HeldTool.GetComponent<MeshFilter>().mesh = NewMesh;
    }
}
