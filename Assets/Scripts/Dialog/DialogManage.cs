using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//主对话框
public class DialogManage : MonoBehaviour
{
    public Text content;
    public GameObject container;
    public Transform back;
	// Use this for initialization
	void Awake ()
    {
        container.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int SetText(string str)
    {
        content.text = str;
        return -1;
    }
}
