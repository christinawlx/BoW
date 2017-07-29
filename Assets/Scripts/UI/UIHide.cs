using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHide : MonoBehaviour {

    public GameObject black;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BlackToNomal()
    {
        PlayerController.ban_all_actions = true;
        black.gameObject.SetActive(true);
        black.GetComponent<Image>().CrossFadeAlpha(1.0f, 0.0f, false);
        black.GetComponent<Image>().CrossFadeAlpha(0.0f, 1.0f, false);
        Invoke("EnableControl",1.0f);
    }

    public void NormalToBlack()
    {
        PlayerController.ban_all_actions = true;
        black.gameObject.SetActive(true);
        black.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.0f, false);
        black.GetComponent<Image>().CrossFadeAlpha(1.0f, 1.0f, false);
        Invoke("EnableControl", 1.0f);
    }

    private void EnableControl()
    {
        PlayerController.ban_all_actions = false;
    }
}
