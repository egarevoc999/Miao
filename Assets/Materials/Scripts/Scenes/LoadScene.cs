using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LoadScene : MonoBehaviour
{
    public Tilemap tilemap;

    public Tile stoneTile;
    public Tile grassTile;
    public Tile river0Tile;
    public Tile river1Tile;
    public Tile river2Tile;
    public Tile river3Tile;
    public Tile river4Tile;
    public Tile river5Tile;
    public Tile river6Tile;
    public Tile river7Tile;
    public Tile river8Tile;
    public Tile river9Tile;
    public Tile river10Tile;
    public Tile river11Tile;
    public Tile river12Tile;
    public Tile river13Tile;
    public Tile river14Tile;
    public Tile river15Tile;
    public Tile hillsTile;
    public Tile treesTile;
    public Tile mountTile;

    List<Block> blockData;
    private Dictionary<int, Tile> riverTypes; 

    // Use this for initialization
    void Start()
    {
        Debug.Log("[LoadScene] start");

        // init river types
        Type t = typeof(LoadScene);
        riverTypes = Enumerable.Range(0, 16).ToDictionary(i => i, i => (Tile)t.GetField($"river{i}Tile").GetValue(this));

        //StartCoroutine(LoadData());
        LoadData();
    }

    private void Update()
    {
        // debug
        // 获取tilemap世界坐标原点
        Vector3 tilemapOrigin = tilemap.transform.position;
        Vector3 xPosDir = tilemapOrigin + tilemap.cellSize.x * Vector3.right;
        Vector3 yPosDir = tilemapOrigin + tilemap.cellSize.x * Vector3.up;
        // 绘制坐标原点及 X 和 Y 正方向
        Debug.DrawLine(tilemapOrigin, xPosDir, Color.red, 100f);  // 红色表示 X 轴正方向
        Debug.DrawLine(tilemapOrigin, yPosDir, Color.green, 100f);    // 绿色表示 Y 轴正方向
    }

    //private IEnumerator LoadData()
    private void LoadData()
    {
        // 加载JSON文件
        JsonParser parser = new JsonParser("start_info");
        parser.Parse();
        Debug.Log("[LoadScene] gamer id: " + parser.GetInfo().self.gamerId + ", name: " + parser.GetInfo().self.name + ", id: " + parser.GetInfo().self.id);

        blockData = parser.GetInfo().blocks;
        UpdateBlocks();
    }

    private void UpdateBlocks()
    {
        for (int i = 0; i < blockData.Count; i++)
        {
            var block = blockData[i];

            // TODO z轴位置暂时硬编码，和平面位置保持一致
            Vector3Int tilePos = new Vector3Int(-block.loc.x-1, -block.loc.y-1, 0);

            // TODO 待处理building/entity/canSee

            // 根据类型设置Tile
            switch (block.type)
            {
                case "STONE":
                    tilemap.SetTile(tilePos, stoneTile);
                    //Debug.Log("stone tilePos: " + tilePos.ToString());
                    break;
                case "GRASS":
                    tilemap.SetTile(tilePos, grassTile);
                    //Debug.Log("grass tilePos: " + tilePos.ToString());
                    break;
                case "RIVER":
                    UpdateRiver(tilePos);
                    break;
                case "HILLS":
                    tilemap.SetTile(tilePos, hillsTile);
                    //Debug.Log("hills tilePos: " + tilePos.ToString());
                    break;
                case "TREES":
                    tilemap.SetTile(tilePos, treesTile);
                    //Debug.Log("trees tilePos: " + tilePos.ToString());
                    break;
                case "MOUNT":
                    tilemap.SetTile(tilePos, mountTile);
                    //Debug.Log("mount tilePos: " + tilePos.ToString());
                    break;
                default:
                    Debug.Log("invaid block type");
                    break;
            } 
        }
    }

    private void UpdateRiver(Vector3Int tilePos)
    {
        Location pos = new Location(tilePos);
        Type t = typeof(LoadScene);
        int mask = CheckNeighbors(pos);
        //Debug.Log("----river tilePos: " + tilePos + ", mask: " + mask);
        Tile tileT = (Tile)t.GetField($"river{mask}Tile").GetValue(this);
        tilemap.SetTile(tilePos, tileT);
    }

    private int CheckNeighbors(Location tilePos)
    {
        int mask = 0;

        (int offsetX, int offsetY)[] directions = new (int, int)[] {
            (1, 0),(0, -1),(-1, 0),(0, 1)
        };

        int cnt = 0b1000;
        foreach (var (offsetX, offsetY) in directions)
        {
            int neighborX = tilePos.x + offsetX;
            int neighborY = tilePos.y + offsetY;
            Block neighbor = blockData.FirstOrDefault(block => (-block.loc.x - 1) == neighborX && (-block.loc.y - 1) == neighborY);
            if (neighbor != null)
            {
                //Debug.Log("found neighbor! current tilePosx: " + tilePos.x + " tilePosy" + tilePos.y + ", neighborx: " + neighborX + ", neightbory: " + neighborY);
                if (neighbor.type == "RIVER")
                {
                    //Debug.Log("neighbor is river");
                    mask |= cnt;
                }
            }
            //else
            //{
            //    Debug.Log("neighbor is null");
            //}
            cnt >>= 1;
        }

        return mask;
    }
}
