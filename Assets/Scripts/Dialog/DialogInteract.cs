using UnityEngine;
using System.Collections;

//对话点
public class DialogInteract: MonoBehaviour
{
    //对话过程变量
    static bool isOpen = false;//是否已在对话
    bool playerIn = false;//玩家是否处在Trigger中

    //外部引用
    DialogManage dialog;
    Transform back;
    PlayerController m_player;
    public Transform[] dialogPoint;
    public SpecialLogicInterface end_logic;

    //对话内容,可从多个里选
    public string m_swName="default";
    public string[] content;

    // Use this for initialization
    void Awake ()
    {
        m_player = GameObject.FindObjectOfType<PlayerController>();
	}

    private void Start()
    {
        dialog = Global.m_dialog;
        if (dialogPoint.Length > 0)
        {
            dialogPoint[0] = Global.m_dialogPoint;
        }
    }

    // Update is called once per frame
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!m_player.isMoving&&!PlayerController.ban_all_actions)
        {
            ChatManage();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        playerIn = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        playerIn =false;
    }

    void ChatManage()
    {
        if (!isOpen)
        {
            //若玩家在Trigger范围内，则开启对话
            if (playerIn)
                StartDialog();
        }
    }

    public void StartDialog()
    {
        isOpen = true;
        //Debug.Log("dialog"+Global.m_dialog);
        //Debug.Log("container"+Global.m_dialog.container);
        if (Global.m_dialog == null)
        {
            Global.m_dialog = FindObjectOfType<DialogManage>();
            dialog = Global.m_dialog;
        }
        Global.m_dialog.container.SetActive(true);
        //禁用一切玩家控制
        PlayerController.isused = false;
        int temp = int.Parse(content[0].Substring(0, 1));
        dialog.transform.position = Camera.main.WorldToScreenPoint(dialogPoint[temp].position);
        dialog.back.GetComponent<RectTransform>().rotation = dialogPoint[temp].rotation;
        dialog.SetText(content[0].Substring(1, content[0].Length - 1));
        StartCoroutine(ManageContent());
    }

    IEnumerator ManageContent()
    {
        yield return new WaitForFixedUpdate();
        for (int i = 0; i < content.Length; i++)
        {
            int temp=int.Parse(content[i].Substring(0,1));
            dialog.transform.position= Camera.main.WorldToScreenPoint(dialogPoint[temp].position);
            dialog.back.GetComponent<RectTransform>().rotation = dialogPoint[temp].rotation;
            dialog.SetText(content[i].Substring(1,content[i].Length-1));
            yield return new WaitForFixedUpdate();
            yield return new WaitWhile(WaitForInteract);
        }
        EndDialog();
    }

    public static bool WaitForInteract()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void EndDialog()
    {
        isOpen = false;
        dialog.container.SetActive(false);
        PlayerController.isused=true;
        ManageSwitch();

        if (end_logic != null)
        {
            end_logic.m_InteractEvent.Invoke();
        }
    }

    void ManageSwitch()
    {
        if (m_swName != "default")
        {
            GameObject.FindObjectOfType<SwitchGroup>().SetSwitch(m_swName,true);
        }
    }
}
