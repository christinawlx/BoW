using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridTest : MonoBehaviour
{
    public BlindTest main_control;
    public GameObject ok;

    public bool isblack = false;
    public bool isclicked = false;
    // Use this for initialization
    void Awake ()
    {
        ok = transform.GetChild(0).gameObject;
        ok.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public bool SetColor()
    {
        float test = Random.Range(0.0f,1.0f);
        float colorVal = 1-1/Mathf.Pow(4,main_control.curLev);
        colorVal -= Random.Range(0, 0.01f);
        if (test < 0.5f)
        {
            isblack = true;
            GetComponent<Image>().color = new Color(colorVal*0.5f, colorVal * 0.5f, colorVal * 0.5f,1.0f);
        }
        else
        {
            isblack = false;
            GetComponent<Image>().color = new Color(1 - colorVal * 0.5f, 1 - colorVal * 0.5f, 1 - colorVal * 0.5f, 1.0f);
        }
        isclicked = false;
        ok.SetActive(false);

        return isblack;
    }

    public void GetClick()
    {
        if (!isclicked)
        {
            if (isblack)
            {
                ok.SetActive(true);
                isclicked = true;
                main_control.GetBlack();
            }
            else
            {
                main_control.GetWhite();
            }
        }
 
    }
}
