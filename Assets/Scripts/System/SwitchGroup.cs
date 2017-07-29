using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MySwitch
{
    public string m_name;
    public bool value;

    public MySwitch()
    {
        value = false;
    }
    public MySwitch(string n,bool val)
    {
        m_name = n;
        value = val;
    }
};
public class SwitchGroup : MonoBehaviour
{
    public List<MySwitch> AllSwitch=new List<MySwitch>();
    // Use this for initialization
    void Awake ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetSwitch(string sw_name, bool val)
    {
        for (int i = 0; i < AllSwitch.Count; i++)
        {
            if (AllSwitch[i].m_name == sw_name)
            {
                AllSwitch[i].value = val;
                return;
            }
        }
        AllSwitch.Add(new MySwitch(sw_name,val));
    }
    public bool GetSwitch(string sw_name)
    {
        for(int i = 0; i < AllSwitch.Count; i++)
        {
            if (AllSwitch[i].m_name == sw_name)
                return AllSwitch[i].value;
        }
        return false;
    }
}
