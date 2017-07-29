using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CreativeSpore.SuperTilemapEditor;

public class PlayerController : MonoBehaviour
{

    public MapManager mainTile;//地图管理模块

    Vector3 tarGrid;//目标格子，当玩家应该移动时，总是向着该格子移动
    Vector3 tarPos;//目标格的坐标
    public bool isMoving=false;//该变量为真时，移动玩家
    bool lightOn = true;//灯光是否打开
    bool wait_E = false;//一旦按下E，准备关灯，等到停止移动时再切换
    public int color = 0;//当前玩家颜色

    public static bool isused = true;//是否禁用玩家(只有交互键保留)
    public static bool ban_all_actions=false;//是否禁用玩家的所有操作(禁用任何操作)

    public float speed=0.1f;//移动速度
    public Transform player_show;
    public List<Light> lights;
    public Transform dialog_Point;
    public GameObject eyeMark;

    private void Awake()
    {
        mainTile = GameObject.FindObjectOfType<MapManager>();
    }
    //初始化
    void Start ()
    {
        Transform LightContainer = GameObject.FindWithTag("LightContainer").transform;
        for(int i = 0; i < LightContainer.childCount; i++)
        {
            lights.Add(LightContainer.GetChild(i).GetComponent<Light>());
        }
        lightOn = false;
        AttachLight(false);
    }
	
	//每帧调用
	void Update ()
    {
        if (!isused)
            return;
        ManageInput();
        ManageMove();
	}

    //处理玩家移动
    void ManageMove()
    {
        if (isMoving)
        {
            //根据位置移动
            Vector3 delta = (tarPos- transform.position).normalized;
            transform.Translate(delta*speed*Time.deltaTime*50);
            //检验是否到达了或者超过了点，默认纵横中只有一个方向有位移
            Vector3 aftdelta= (tarPos-transform.position).normalized;
            if (Vector3.Dot(delta, aftdelta) <= 0)
            {
                transform.position = tarPos;
                UpdateColor();
                isMoving = false;
            }
        }
    }

    //处理玩家输入
    void ManageInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            wait_E = true;
        }
        if (!isMoving)
        {
            //处理移动
            Vector3 dir = ChooseDir();
            if (dir.x != 0.0f || dir.y != 0.0f)
            {
                Vector3 resGrid = mainTile.MovePosWithDir(transform.position,dir,color);

                bool skipAttach = false;
                for (int i = 0; i < lights.Count; i++)
                {
                    //若玩家在灯光里
                    if (lights[i].TestIn(transform.position))
                    {
                        if (lights[i].is_horizon)
                        {
                            if (lights[i].StartPoint.y == tarGrid.y && dir.y < 0.0
                                || lights[i].EndPoint.y == tarGrid.y && dir.y > 0.0)
                            {
                                skipAttach = true;
                                break;
                            }
                        }
                        if (!lights[i].is_horizon)
                        {
                            if (lights[i].StartPoint.x == tarGrid.x && dir.x < 0.0
                               || lights[i].EndPoint.x == tarGrid.x && dir.x > 0.0)
                            {
                                skipAttach = true;
                                break;
                            }
                        }
                    }
                }

                if (!lightOn&&resGrid.z > 0)
                {
                    tarGrid.x = resGrid.x;
                    tarGrid.y = resGrid.y;
                    tarPos = mainTile.ConvertGridToPos(tarGrid);
                    isMoving = true;
                }
                else if(lightOn&& resGrid.z >= 0)
                //else if(resGrid.z==0&&lightOn)
                {
                    bool skipUnAttach=false;

                    //检验绑定所有灯光是否可通行
                    if (!skipAttach)
                    {
                        AttachLight(true);
                        resGrid = mainTile.MovePosWithDir(transform.position, dir, color);
                        if (resGrid.z > 0)
                        {
                            tarGrid.x = resGrid.x;
                            tarGrid.y = resGrid.y;
                            tarPos = mainTile.ConvertGridToPos(tarGrid);
                            isMoving = true;
                            ////绑定可通行则不检验未绑定
                            //skipUnAttach = true;
                        }
                    }
                    //检验如果全部解除灯光绑定是否可通行
                    if (!skipUnAttach)
                    {
                        mainTile.ApplyAllLights();
                        UpdateColor();
                        resGrid = mainTile.MovePosWithDir(transform.position, dir, color);
                        if (resGrid.z > 0)
                        {
                            //是则解除灯光绑定
                            tarGrid.x = resGrid.x;
                            tarGrid.y = resGrid.y;
                            tarPos = mainTile.ConvertGridToPos(tarGrid);
                            isMoving = true;
                            for (int i = 0; i < lights.Count; i++)
                            {
                                lights[i].EndAttach();
                            }
                        }
                        else
                        {
                            //否则检验绑定所有灯光是否可通行
                            mainTile.UpdateMapWithLights();
                            UpdateColor();
                        }
                    }
                }
            }

            //处理开关灯情况
            if (wait_E)
            {
                wait_E = false;
                if (lightOn)
                {
                    AttachLight(false);
                    eyeMark.SetActive(false);
                }
                else
                {
                    AttachLight(true);
                    eyeMark.SetActive(true);
                }
               //UpdateConstrain();
            }
        }
    }
    //判断玩家输入方向
    Vector3 ChooseDir()
    {
        Vector3 InputDir = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            InputDir.x = -1;
            player_show.localRotation =Quaternion.Euler(0.0f,180.0f,0.0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            InputDir.x = 1;
            player_show.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        else if (Input.GetKey(KeyCode.W))
            InputDir.y = 1;
        else if (Input.GetKey(KeyCode.S))
            InputDir.y = -1;
        return InputDir;
    }
    //控制是否把灯与玩家绑定
    void AttachLight(bool val)
    {
        if (val)
        {
            for (int i = 0; i < lights.Count; i++)
            {
                if (lights[i].TestIn(transform.position))
                {
                    lights[i].BeginAttach();
                }
            }
            mainTile.SetLightSwitch(true);
            lightOn = true;
            UpdateColor();
        }
        else
        {
            for (int i = 0; i < lights.Count; i++)
            {
                lights[i].EndAttach();
            }
            mainTile.SetLightSwitch(false);
            lightOn = false;
            UpdateColor();
        }
    }

    //更新颜色
    public void UpdateColor()
    {
        int incolor = mainTile.ConverPosToColor(transform.position);
        if (incolor == 0)
            color = 1;
        else if (incolor == 1)
            color = 0;
    }
    //返回玩家现在所处格子
    public Vector3 GetCurGrid()
    {
        return mainTile.ConvertPosToGrid(transform.position);
    }
}
