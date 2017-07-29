using UnityEngine;
using System.Collections;
using CreativeSpore.SuperTilemapEditor;

public struct LogicMapNode
{
    public int x;
    public int y;
    public int color;
    public bool staClose;
};

public class MapManager : MonoBehaviour
{
    float cellsize;
    Tilemap tile;
    LogicMapNode[,] oriLogic;
    LogicMapNode[,] mapLogic;

    Transform LightContainer;
    Light[] lights;

    public Tilemap block;

    //逻辑地图大小
    int mapsizex;
    int mapsizey;
	// Use this for initialization
	void Awake ()
    {
        tile = GetComponent<Tilemap>();
        cellsize = tile.CellSize.x;

        LoadMapData();

        LightContainer = GameObject.FindWithTag("LightContainer").transform;
        lights = new Light[LightContainer.childCount];
        for(int i=0; i < LightContainer.childCount; i++)
        {
            lights[i] = LightContainer.GetChild(i).GetComponent<Light>();
        }
        block.gameObject.SetActive(false);
	}

    void Start()
    {
       
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void LoadMapData()
    {
        //载入地图逻辑
        mapsizex = tile.MaxGridX - tile.MinGridX+1;
        mapsizey = tile.MaxGridY - tile.MinGridY+1;
        mapLogic = new LogicMapNode[mapsizex, mapsizey];
        oriLogic= new LogicMapNode[mapsizex, mapsizey];

        //暂时先只配置一张地图，通行情况只有黑白两种
        uint rawTileData;
        TileData data;
        for (int i = 0; i < mapsizex; i++)
        {
            for (int j = 0; j < mapsizey; j++)
            {
                //格子坐标
                mapLogic[i, j].x = i + tile.MinGridX;
                mapLogic[i, j].y = j + tile.MinGridY;
                //格子信息
                rawTileData = tile.GetTileData(mapLogic[i, j].x, mapLogic[i, j].y);
                data = new TileData(rawTileData);
                if (data.tileId == 0)//黑色普通
                {
                    mapLogic[i, j].color = 0;
                }
                else if (data.tileId == 5)
                {
                    mapLogic[i, j].color = 1;
                }
                //读取block信息
                rawTileData = block.GetTileData(mapLogic[i, j].x, mapLogic[i, j].y);
                data = new TileData(rawTileData);
                if (data.tileId == 0)
                {
                    mapLogic[i, j].staClose = true;
                }

                //备份原始数据
                oriLogic[i, j].x = mapLogic[i, j].x;
                oriLogic[i, j].y = mapLogic[i, j].y;
                oriLogic[i, j].color = mapLogic[i, j].color;
                oriLogic[i, j].staClose = mapLogic[i, j].staClose;
            }
        }
    }
    void ResetMapLogic()
    {
        for (int i = 0; i < mapsizex; i++)
        {
            for (int j = 0; j < mapsizey; j++)
            {
                mapLogic[i, j].color = oriLogic[i, j].color;
            }
        }
    }

    void SetLight(Light l)
    {
        int sx = (int)l.StartPoint.x;
        int sy= (int)l.StartPoint.y;
        int ex = (int)l.EndPoint.x;
        int ey = (int)l.EndPoint.y;
        for(int i = sx; i <= ex; i++)
        {
            for(int j = sy; j <=ey; j++)
            {
                Vector2 lo = ConvertGridToLogic(new Vector2(i,j));
                //Debug.Log("X" + lo.x + "Y" + lo.y);
                if (mapLogic[(int)lo.x, (int)lo.y].color == 0)
                    mapLogic[(int)lo.x, (int)lo.y].color = 1;
                else if(mapLogic[(int)lo.x, (int)lo.y].color==1)
                    mapLogic[(int)lo.x, (int)lo.y].color = 0;
            }
        }
    }

    //将世界坐标转化为格子坐标
    public Vector3 ConvertPosToGrid(Vector3 pos)
    {
        //返回三个值，最后一个标志着该格子是否已经超出地图范围。
        float gridx = Mathf.Floor((pos.x - transform.position.x)/cellsize);
        float gridy = Mathf.Floor((pos.y - transform.position.y) / cellsize);
        Vector3 res=new Vector3(gridx,gridy,0.0f);
        if (gridx >= tile.MinGridX && gridx <= tile.MaxGridX && gridy >= tile.MinGridY && gridy <= tile.MaxGridY)
        {
            res.z = 1.0f;
        }
        else
        {
            res.z = -1.0f;
        }
        return res;
    }
    //将格子坐标转化为世界坐标
    public Vector3 ConvertGridToPos(Vector3 grid)
    {
        Vector3 res=Vector3.zero;
        res.x = grid.x + 0.5f;
        res.y = grid.y + 0.5f;
        return res;
    }
    //将格子坐标转化为数组逻辑坐标
    public Vector2 ConvertGridToLogic(Vector3 grid)
    {
        return new Vector2(grid.x-tile.MinGridX,grid.y-tile.MinGridY);
    }
    //返回某点的逻辑颜色
    public int ConverPosToColor(Vector3 pos)
    {
        Vector3 grid = ConvertPosToGrid(pos);
        Vector2 lo = ConvertGridToLogic(grid);
        return mapLogic[(int)lo.x,(int)lo.y].color;
    }

    //根据输入信息以及点返回目标点及通行状态
    public Vector3 MovePosWithDir(Vector3 pos,Vector3 dir,int color)
    {
        Vector3 grid = ConvertPosToGrid(pos);
        grid = grid + dir;
        Vector2 Logic = ConvertGridToLogic(grid);
        bool outRange = grid.x > tile.MaxGridX || grid.x < tile.MinGridX || grid.y > tile.MaxGridY || grid.y < tile.MinGridY;

        if (outRange)
        {
            return new Vector3(grid.x, grid.y, -1);
        }

        LogicMapNode lo = mapLogic[(int)Logic.x, (int)Logic.y];
        if (lo.staClose)
        {
            //无论如何不可通行
            return new Vector3(grid.x,grid.y,-1);
        }
        else if (lo.color == color)
        {
            //因为同色不可通行
            return new Vector3(grid.x,grid.y,0);
        }
        else
        {
            //可以通行
            return new Vector3(grid.x,grid.y,1);
        }
    }
    //根据灯光更新地图逻辑
    public void UpdateMapWithLights()
    {
        //重置地图逻辑
        ResetMapLogic();
        //根据每一个灯光进行处理
        for(int i = 0; i < lights.Length; i++)
        {
            if (lights[i].is_active&&!lights[i].attach_to_player)
            {
                SetLight(lights[i]);
            }
        }
    }
    //处理地图灯光的开关
    public void SetLightSwitch(bool val)
    {
        if (val)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetEnable(true);
            }
            UpdateMapWithLights();
        }
        else
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetEnable(false);
            }
            ResetMapLogic();
        }
    }
    //将所有灯光实装
    public void ApplyAllLights()
    {
        //重置地图逻辑
        ResetMapLogic();
        //根据每一个灯光进行处理
        for (int i = 0; i < lights.Length; i++)
        {
            if (lights[i].enabled)
            {
                SetLight(lights[i]);
            }
        }
    }
}
