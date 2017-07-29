using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FragmentSystem : MonoBehaviour
{
    public static bool[] itemGet = new bool[4]; 
    public Text crystalNum;
    public GameObject[] sp;
	// Use this for initialization
	void Start () {
        for(int i = 0; i < itemGet.Length; i++)
        {
            itemGet[i] = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateShow(int crystal,int frag)
    {
        //crystalNum.text =crystal.ToString();
        for(int i = 0; i < sp.Length; i++)
        {
            if (i < frag)
            {
                sp[i].SetActive(true);
            }
            else
            {
                sp[i].SetActive(false);
            }
        }
    }
}
