using UnityEngine;
using UnityEngine.Tilemaps;

public class Summon
{
    private Tilemap overlayTilemap;

    private Entity entity;

    private Tile entityTile;

    private Vector3Int targetPos;

    public Summon(Entity entity, Tilemap targetTilemap)
    {
        this.entity = entity;
        this.overlayTilemap = targetTilemap;
    }

    public void Start()
    {
        Debug.Log("Start summon");
        LoadEntitySprite();
        LoadPos();
        PlacePriteOnTilemap();
    }

    private void LoadEntitySprite()
    {
        // TODO 根据entity-card类型找到对应的sprite资源，现在先硬编码使用小无猫
        string path = "Entities/xiaowu";
        this.entityTile = Resources.Load<Tile>(path);
        if (this.entityTile == null)
        {
            Debug.LogError($"Tile not found at path :{path}");
        }
    }

    private void LoadPos()
    {
        this.targetPos = new Vector3Int(-this.entity.loc.x - 1, -this.entity.loc.y - 1, 0);
        Debug.Log("Summon pos: " + this.targetPos.ToString());
    }

    private void PlacePriteOnTilemap()
    {
        if (this.entityTile == null)
        {
            return;
        }
        overlayTilemap.SetTile(this.targetPos, this.entityTile);
    }

    //private void PlacePriteOnTilemap()
    //{
    //    // 获取tilemap的世界坐标
    //    Vector3 tilemapPos = tilemap.GetCellCenterWorld(this.targetPos);

    //    // 创建新的空对象，用于渲染sprite
    //    GameObject spriteObj = new GameObject("CustomSpriteOverlay");
    //    SpriteRenderer spriteRenderer = spriteObj.AddComponent<SpriteRenderer>();
    //    spriteRenderer.sprite = this.entitySprite;
    //    spriteRenderer.sortingOrder = 10;  // 在tilemap上方

    //    // 指定目标位置
    //    spriteObj.transform.position = tilemapPos;

    //    // 确保与tilemap同一层级
    //    spriteObj.transform.parent = tilemap.transform;
    //}
}
