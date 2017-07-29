using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPage : MonoBehaviour {

    public Image black;
    public Text keydown;
	// Use this for initialization
	void Start () {
        fade1();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            startgame();
        }
	}

    void startgame()
    {
        black.gameObject.SetActive(true);
        black.CrossFadeAlpha(0.0f, 0.0f, false);
        black.CrossFadeAlpha(1.0f,2.0f,false);
        Invoke("ChangeLevel",2.0f);
    }

    void ChangeLevel()
    {
        Application.LoadLevel("00InnaRoom");
    }

    void fade1()
    {
        keydown.CrossFadeAlpha(0.0f,1.5f,false);
        Invoke("fade2",1.5f);
    }

    void fade2()
    {
        keydown.CrossFadeAlpha(1.0f, 1.5f, false);
        Invoke("fade1", 1.5f);
    }
}
