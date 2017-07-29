using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChange : MonoBehaviour {
    public string lvName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Application.LoadLevel(lvName);
        }	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Application.LoadLevel(lvName);
    }
}
