using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shining : MonoBehaviour
{
    public bool is_shining=true;
    public bool is_show = true;
    public GameObject targetMain;
    public GameObject target;
	// Use this for initialization
	void Start ()
    {
        Shining1();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (is_show)
        //{
        //    if (!targetMain.gameObject.activeSelf)
        //    {
        //        targetMain.gameObject.SetActive(true);
        //    }
        //}
        //else
        //{
        //    if (targetMain.gameObject.activeSelf)
        //    {
        //        targetMain.gameObject.SetActive(false);
        //    }
        //}
    }

    void Shining1()
    {
   
        if (is_shining)
        {
            target.SetActive(true);
            Invoke("Shining2", 0.5f);
        }
    }

    void Shining2()
    {
        if (is_shining)
        {
            target.SetActive(false);
            Invoke("Shining1", 0.5f);
        }
    }

    public void StartShining()
    {
        is_shining = true;
        Shining1();
    }

    public void EndShining()
    {
        is_shining = false;
        target.SetActive(true);
    }
}
