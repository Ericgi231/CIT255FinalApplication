using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashTracker : MonoBehaviour {

    static public float Money { get; set; }
    private float OldMoney { get; set; }

    // Use this for initialization
    void Start () {
        OldMoney = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Money > OldMoney)
        {
            UpdateCashView();
        }
	}

    void UpdateCashView()
    {
        GameObject.Find("CashViewer").GetComponent<TextMesh>().text = "$ " + Money.ToString().PadLeft(7,'0');
    }
}
