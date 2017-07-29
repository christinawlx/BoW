using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameFlowControl : MonoBehaviour
{
    //在此处做统一的关卡初始化处理，防止乱七八糟的顺序问题，待后期修改

    //场景赋值信息
    public Transform ExitContain;//场景切换口容器
    Transform[] startPos;//场景切换口
    public Transform LightContainer;//灯光容器
    public Transform SavedObjContainer;//需要存储销毁状态的物体
    public bool ControlInit = false;//是否由外部特殊脚本调用初始化

    //写入场景存档的信息
    [HideInInspector]public bool[] obj_destroy;//某物品是否销毁
    [HideInInspector]public SwitchGroup pri_switch;//当前地图开关

    // Use this for initialization
    void Awake()
    {
        //为全局变量赋值
        Global.SetPlayer(GameObject.FindWithTag("Player").transform);
        Global.SetPlayerDialogPoint(Global.m_player.GetComponent<PlayerController>().dialog_Point);
        Global.m_attributes = GameObject.FindObjectOfType<PlayerAttributes>();
        Global.m_gameFlow = this;

        //初始化场景切换口
        int num = ExitContain.childCount;
        startPos = new Transform[num];
        for (int i = 0; i < num; i++)
        {
            startPos[i] = ExitContain.GetChild(i);
        }
    }
	void Start ()
    {
        if (!ControlInit)
        {
            ControlledInit();
        }

        //根据存档销毁物品
        DestroySavedObj();
        //屏幕渐显
        Global.m_ui.SetFadeBlack(false);
	}

    // Update is called once per frame
    void Update ()
    {
	
	}

    //更新玩家位置
    void PlayerPosManage()
    {
        //防止刚进入就返回
        if (startPos[SceneChange.pub_reID].GetComponent<SceneChange>() != null)
        {
            startPos[SceneChange.pub_reID].GetComponent<SceneChange>().once = true;
        }
        //根据场景入口信息初始化玩家位置
        Global.m_player.position = startPos[SceneChange.pub_reID].position;
        //重置场景切换信息
        SceneChange.should_update = false;
        SceneChange.pub_reID = -1;
    }
    //导入存储信息
    void LoadDataMain()
    {
        SingleScene temp=Global.m_sceneData.GetDataBySceneName(Application.loadedLevelName);
        if (temp != null)
        {
            obj_destroy = temp.obj_destroy;
            pri_switch = temp.pri_switch;
            LoadLightData(temp.light_data);
        }
    }
    void LoadLightData(LevelLightData temp)
     {
        //读取已保存的灯光信息，更新玩家颜色
        temp.ApplyLightData(LightContainer);
        Global.m_player.GetComponent<PlayerController>().UpdateColor();
    }

    void LoadOriginDestroy()
    {
        obj_destroy = new bool[SavedObjContainer.childCount];
        for (int i = 0; i < SavedObjContainer.childCount; i++)
        {
            Transform temp = SavedObjContainer.GetChild(i);
            if (temp.GetComponent<SavedObject>() != null)
            {
                obj_destroy[temp.GetComponent<SavedObject>().id] = temp.GetComponent<SavedObject>().is_destroy;
            }
        }
    }

    //根据存档销毁注册物体
    void DestroySavedObj()
    {
        for(int i=0;i< SavedObjContainer.childCount; i++)
        {
            Transform temp = SavedObjContainer.GetChild(i);
            if (temp.GetComponent<SavedObject>() != null)
            {
                if (obj_destroy[temp.GetComponent<SavedObject>().id])
                {
                    temp.gameObject.SetActive(false);
                }
            }
        }
    }

    //保存当前场景信息
    public void SaveData()
    {
        SingleScene temp = new SingleScene();
        temp.s_name = Application.loadedLevelName;
        temp.obj_destroy = obj_destroy;
        temp.pri_switch = pri_switch;
        temp.light_data.SaveDataWithContainer(LightContainer);
        Global.m_sceneData.UpdateData(temp);
    }

    //可外部控制的过程
    public void ControlledInit()
    {
        if (SceneChange.should_update)
        {
            //初始化玩家位置
            PlayerPosManage();
            //初始化地图存档信息
            LoadDataMain();
        }
        else
        {
            LoadOriginDestroy();
        }
    }
}
