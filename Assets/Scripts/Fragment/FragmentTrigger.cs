using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentTrigger : MonoBehaviour
{
    public static bool show_frag=false;
    public int fragmentid=0;

    public bool my_active = true;
	// Use this for initialization
	void Start ()
    {
        if (FragmentSystem.itemGet[fragmentid])
        {
            Destroy(gameObject);
            return;
        }

        CheckShow();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(0.0f,2.0f,0.0f));	
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FragmentSystem.itemGet[fragmentid] = true;
            Global.m_attributes.GetFragment();
            Destroy(gameObject);
        }
    }

    public void CheckShow()
    {
        if (show_frag)
        {
            MySetActive(true);
        }
        else
        {
            MySetActive(false);
        }
    }

    public void MySetActive(bool val)
    {
        GetComponent<BoxCollider2D>().enabled = val;
        transform.GetChild(0).gameObject.SetActive(val);
        my_active = val;
    }
}
