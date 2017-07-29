using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//记录一个灯光
public struct SingleLightData
{
    public Vector3 StartPoint;
    public Vector3 EndPoint;
    public bool is_hor;
};

//记录每关的灯光
[System.Serializable]
public class LevelLightData
{
    public string LevName;
    [HideInInspector]public List<SingleLightData> LightGroup=new List<SingleLightData>();

    public void SaveDataWithContainer(Transform lightContain)
    {
        LightGroup.Clear();
        //将当前关卡灯光信息存在对应位置
        for (int j = 0; j < lightContain.childCount; j++)
        {
            Light temp=lightContain.GetChild(j).GetComponent<Light>();
            SingleLightData n_data;
            n_data.StartPoint = temp.StartPoint;
            n_data.EndPoint = temp.EndPoint;
            n_data.is_hor = temp.is_horizon;
            LightGroup.Add(n_data);
        }
    }

    public void ApplyLightData(Transform lightContain)
    {
        if (LightGroup.Count == 0) return;
        //将当前灯光信息应用到所有灯光
        for (int j = 0; j < lightContain.childCount; j++)
        {
            //此处存疑，可能会导致设置不了light
            Light temp = lightContain.GetChild(j).GetComponent<Light>();
            temp.SetLightData(LightGroup[j].StartPoint,LightGroup[j].EndPoint,LightGroup[j].is_hor);
        }
        //此处后期可删除，改到统一初始化函数里
        GameObject.FindObjectOfType<MapManager>().UpdateMapWithLights();
    }
};

//记录所有灯光
public class LightSaveData : MonoBehaviour
{
    public LevelLightData[] all_light_data;
    //绑在系统控制物体上，确定只出现一次
    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //外部调用
    //存储灯光
    public void SaveLight()
    {
        Transform lightContain = GameObject.FindWithTag("LightContainer").transform;
        string levName = Application.loadedLevelName;
        for(int i = 0; i < all_light_data.Length; i++)
        {
            if (all_light_data[i].LevName==levName)
            {
                //all_light_data[i].SetDataWithContainer(lightContain);
            }
        }
    }

    //读取灯光
    public void LoadLight()
    {
        Transform lightContain = GameObject.FindWithTag("LightContainer").transform;
        string levName = Application.loadedLevelName;
        for (int i = 0; i < all_light_data.Length; i++)
        {
            if (all_light_data[i].LevName == levName)
            {
                all_light_data[i].ApplyLightData(lightContain);
            }
        }
    }
}
