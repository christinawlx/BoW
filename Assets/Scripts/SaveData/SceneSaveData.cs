using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
public class SingleScene
{
    public string s_name;
    public bool[] obj_destroy;
    public SwitchGroup pri_switch;
    public LevelLightData light_data=new LevelLightData();
}

public class SceneSaveData : MonoBehaviour
{
    List<SingleScene> s_data=new List<SingleScene>();
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void UpdateData(SingleScene single)
    {
        for(int i = 0; i < s_data.Count; i++)
        {
            if (s_data[i].s_name == single.s_name)
            {
                s_data[i] = single;
                return;
            }
        }
        s_data.Add(single);
    }

    public SingleScene GetDataBySceneName(string s_name)
    {
        for (int i = 0; i < s_data.Count; i++)
        {
            if (s_data[i].s_name == s_name)
            {
                return s_data[i];
            }
        }
        return null;
    }
}
