using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    //此三项为每关开始，由关卡内部固定赋值
    public static Transform m_dialogPoint;//该关卡玩家位置的对话点
    public static Transform m_player;//该关卡的玩家
    public static GameFlowControl m_gameFlow;

    //这些项项为游戏开始，统一赋值
    public static PlayerAttributes m_attributes;//玩家属性

    public static FragmentSystem m_frag;//碎片系统
    public static DialogManage m_dialog;//对话系统
    public static UIInterface m_ui;//UI系统

    public static SwitchGroup m_pubSwitches;//公共开关存档
    public static SceneSaveData m_sceneData;//场景存档

    public static bool once=false;

	// Use this for initialization
	void Awake ()
    {
        if (!once)
        {
            DontDestroyOnLoad(gameObject);
            once = true;
        }
        else
        {
            Destroy(gameObject);
        }
        m_dialog = GameObject.FindObjectOfType<DialogManage>();
        m_frag = GameObject.FindObjectOfType<FragmentSystem>();
        m_ui = GameObject.FindObjectOfType<UIInterface>();
        m_pubSwitches = GameObject.FindObjectOfType<SwitchGroup>();
        m_sceneData = GameObject.FindObjectOfType<SceneSaveData>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public static void SetPlayerDialogPoint(Transform dialog_p)
    {
        m_dialogPoint = dialog_p;
    }
    public static void SetPlayer(Transform player)
    {
        m_player = player;
    }
}
