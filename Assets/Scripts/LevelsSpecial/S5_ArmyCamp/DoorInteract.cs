using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteract : MonoBehaviour
{
    bool isopen = false;
    public Transform endPage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (PlayerAttributes.crystalNum == 0)
                {
                    if (!isopen)
                    {
                        isopen = true;
                        FragmentTrigger.show_frag = true;
                        //Global.m_event.OperateEvent("HintCrystal");
                    }
                }
                else
                {
                    Level1Over();
                }
            }
        }
    }

    void Level1Over()
    {
        if (!isopen)
        {
            isopen = true;
            PlayerController.isused = false;
            endPage.gameObject.SetActive(true);
            for (int i = 0; i < endPage.childCount; i++)
            {
                if (endPage.GetChild(i).GetComponent<Image>() != null)
                {
                    endPage.GetChild(i).GetComponent<Image>().CrossFadeAlpha(0.0f, 0.0f, true);
                    endPage.GetChild(i).GetComponent<Image>().CrossFadeAlpha(1.0f, 2.0f, true);
                }
                else if (endPage.GetChild(i).GetComponent<Text>() != null)
                {
                    endPage.GetChild(i).GetComponent<Text>().CrossFadeAlpha(0.0f, 0.0f, true);
                    endPage.GetChild(i).GetComponent<Text>().CrossFadeAlpha(1.0f, 2.0f, true);
                }
            }
        }
    }

    public void ResetOpen()
    {
        isopen = false;
    }
}
