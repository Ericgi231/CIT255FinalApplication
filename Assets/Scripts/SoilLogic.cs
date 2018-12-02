using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilLogic : MonoBehaviour {

    Material mat;
    SoilState state;

    public enum SoilState
    {
        RAW,
        TILLED,
        WATTERED
    }

	// Use this for initialization
	void Start () {
        mat = GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        state = SoilState.WATTERED;
        SetState();
    }

    void SetState()
    {
        switch (state)
        {
            case SoilState.RAW:
                mat.color = new Color32(120,30,0,0);
                break;
            case SoilState.TILLED:
                mat.color = new Color32(150, 60, 0, 0);
                break;
            case SoilState.WATTERED:
                mat.color = new Color32(150, 60, 50, 0);
                break;
            default:
                break;
        }
    }
}
