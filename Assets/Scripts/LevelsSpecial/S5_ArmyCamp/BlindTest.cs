using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindTest : MonoBehaviour
{
    public int TarBlackCount=0;
    public int curBlackCount = 0;
    public int curLev = 0;

    public GameObject content;
    public Transform gridContainer;
    public DialogInteract GameOver;
    public DialogInteract BanPass;

    bool isused = false;
	// Use this for initialization
	void Start ()
    {
        //content.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
       
	}

    public void StartEvent()
    {
        PlayerController.isused = false;
        content.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
        curLev = 0;
        ManageNextLev();
        isused = true;
    }

    public void GetBlack()
    {
        curBlackCount++;
        CheckLevelClear();
    }

    public void GetWhite()
    {
        CurGameOver();
    }

    void CheckLevelClear()
    {
        if (curBlackCount == TarBlackCount)
        {
            curLev++;
            ManageNextLev();
        }
    }

    void CurGameOver()
    {
        if (isused)
        {
            isused = false;
            PlayerController.isused = true;
            content.transform.localPosition = new Vector3(0.0f, 900.0f, 0.0f);
            if (curLev == 0)
            {
                GameOver.StartDialog();
            }
            else
            {
                BanPass.StartDialog();
            }
        }
    }

    void ManageNextLev()
    {
        TarBlackCount = 0;
        curBlackCount = 0;
        for (int i = 0; i < gridContainer.childCount; i++)
        {
            if (gridContainer.GetChild(i).GetComponent<GridTest>().SetColor())
            {
                TarBlackCount++;
            }
        }
    }
}
