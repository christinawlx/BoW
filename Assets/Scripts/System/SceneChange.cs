using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour
{
    //用于标记场景
    public static bool should_update=false;
    public static int pub_reID;
    //外部变量
    public string s_name;
    public int reID=0;
    //判断是否从该口进入
    public bool once=false;
	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!once)
        {
            if (col.tag == "Player")
            {
                should_update = true;
                pub_reID = reID;
                Global.m_gameFlow.SaveData();
                StartCoroutine(loadNext());
            }
        }
    }

    IEnumerator loadNext()
    {
        Global.m_ui.SetFadeBlack(true);
        yield return new WaitForSeconds(1.0f);
        Application.LoadLevel(s_name);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            once = false;
        }
    }
}
