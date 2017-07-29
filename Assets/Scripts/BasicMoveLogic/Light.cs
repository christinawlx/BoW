using UnityEngine;
using System.Collections;

//注意startPoint和EndPoint的大小关系
public class Light : MonoBehaviour
{
    public Vector3 StartPoint;//灯光开始点
    public Vector3 EndPoint;//灯光结束点
    public bool is_horizon=true;//灯光是横向的
    public bool is_active = true;//灯光正在使用
    public bool attach_to_player = false;//灯光与玩家连结

    Vector3 deltaPos;//玩家与灯光的距离
    Vector3 deltaGrid;//玩家与灯光的格子距离
    Vector3 SEdelta;//起点和终点间差值，固定，确定灯光大小

    //两个需要用到其他脚本的地方，玩家与地图
    MapManager map;
    PlayerController player;
    // Use this for initialization
    void Awake ()
    {
        map = GameObject.FindObjectOfType<MapManager>();
        player = GameObject.FindObjectOfType<PlayerController>();

        //记录灯光范围大小
        SEdelta = EndPoint - StartPoint;
        //更新显示
        UpdateShow();
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (attach_to_player)
        {
            UpdateLogic();
        }
	}

    void UpdateLogic()
    {
        //灯光逻辑位置变更
        //不更新被锁定的信息
        if (is_horizon)
        {
            transform.position = new Vector3(player.transform.position.x + deltaPos.x, transform.position.y, 0.0f);
            StartPoint.x = player.GetCurGrid().x + deltaGrid.x;
            EndPoint.x = StartPoint.x + SEdelta.x;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + deltaPos.y, 0.0f);
            StartPoint.y = player.GetCurGrid().y + deltaGrid.y;
            EndPoint.y = StartPoint.y + SEdelta.y;
        }
    }

    //更新灯光显示
    void UpdateShow()
    {
        Vector3 st = map.ConvertGridToPos(StartPoint);
        Vector3 ed = map.ConvertGridToPos(EndPoint);
        transform.position = (st + ed) / 2;
        transform.localScale = new Vector3(Mathf.Abs(EndPoint.x-StartPoint.x)+1, Mathf.Abs(EndPoint.y- StartPoint.y) + 1, 1);
    }

    //灯光与玩家连接时调用
    public void BeginAttach()
    {
        if (!attach_to_player)
        {
            attach_to_player = true;
            deltaPos = transform.position - player.transform.position;//记录玩家与灯光的相对位置，方便更新
            deltaGrid = StartPoint-player.GetCurGrid();//记录玩家与灯光的将对格子距离，方便更新
        }
    }

    //灯光与玩家结束连接时调用
    public void EndAttach()
    {
        if (attach_to_player)
        {
            attach_to_player = false;
            UpdateLogic();
        }
    }

    //激活灯光
    public void SetEnable(bool val)
    {
        is_active = val;
        gameObject.SetActive(val);
    }

    //检测玩家是否在该灯光里
    public bool TestIn(Vector3 pos)
    {
        Vector3 grid = map.ConvertPosToGrid(pos);
        float dx = Mathf.Abs(grid.x - (StartPoint.x + EndPoint.x) / 2);
        float dy= Mathf.Abs(grid.y - (StartPoint.y + EndPoint.y) / 2);
        if (dx <= Mathf.Abs(StartPoint.x-EndPoint.x)/2&&dy<= Mathf.Abs(StartPoint.y - EndPoint.y) / 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetLightData(Vector3 startPoint,Vector3 endPoint,bool h)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
        is_horizon = h;
        UpdateShow();
    }
}
