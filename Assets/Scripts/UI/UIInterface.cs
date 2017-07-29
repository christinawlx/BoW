using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInterface : MonoBehaviour {

    public UIHide m_hide;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetFadeBlack(bool val)
    {
        if (val)
        {
            m_hide.NormalToBlack();
        }
        else
        {
            m_hide.BlackToNomal();
        }
    }
}
