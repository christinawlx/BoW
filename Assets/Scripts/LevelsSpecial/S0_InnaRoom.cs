using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class S0_InnaRoom : MonoBehaviour {
    public DialogInteract FirstStart;
	// Use this for initialization
    //public Image black;
	void Start ()
    {
        bool res = Global.m_pubSwitches.GetSwitch("FirstStart");
        if (!res)
        {
            PlayerController.isused = false;
            PlayerController.ban_all_actions = true;
            //black.CrossFadeAlpha(0.0f, 2.0f, false);
            Invoke("StartPlot", 1.2f);
        }
    }


    //游戏最开始的强制剧情
    void StartPlot()
    {
        PlayerController.ban_all_actions = false;
        FirstStart.StartDialog();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
