using UnityEngine;
using System.Collections;

public class PlayerAttributes : MonoBehaviour
{
    MapManager map;
    public static int fragmentNum=0;
    public static int crystalNum =0;
	// Use this for initialization
	void Start ()
    {
        map = FindObjectOfType<MapManager>();
        //Global.m_frag.UpdateShow(crystalNum, fragmentNum);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void GetFragment()
    {
        fragmentNum++;
        if (fragmentNum == 4)
        {
            crystalNum++;
            //fragmentNum = 0;
        }
        Global.m_frag.UpdateShow(crystalNum,fragmentNum);
    }
}
