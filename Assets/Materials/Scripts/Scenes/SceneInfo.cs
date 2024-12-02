using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SceneInfo
{
    public PlayerInfo self;
    public List<PlayerInfo> others;
    public List<Block> blocks;
}

[Serializable]
public class PlayerInfo
{
    public int gamerId;  // 这局游戏分配的玩家id
    public string name;
    public int id;  // 全局唯一的玩家id
    public Dictionary<Location, Building> buildings;
    public Dictionary<int, Entity> entities;
    public Dictionary<int, Entity> incapacitatedEntities;
    public int energyNum;  // 玩家当前能量值，每回合开始时+i（i默认为3）
    public Dictionary<int, Card> cards;  // 玩家剩余卡牌
    public Dictionary<int, Card> usedCard;  // 已使用的卡牌
    public Building baseNest;  // 基础猫窝
    public Entity gem;  // 宝石
    public bool hasEnd;  // 玩家存活状态
}

[Serializable]
public class Location
{
    public int x;
    public int y;
    public int z;

    public Location(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Location(Vector3Int loc)
    {
        this.x = loc[0];
        this.y = loc[1];
        this.z = loc[2];
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

[Serializable]
public class Building
{
    public Card card;
    public int schedule;
    public Location loc;
    public Owner owner;
    public CatContainer allCat;
    public int remainCapacity;
}

[Serializable]
public class Owner
{
    public int id;  // 玩家id
}

// 前端不需要理解卡牌字段含义，只需要显示出来
[Serializable]
public class Card
{
    public int id;  // 一场游戏中对每张卡牌的编号
    public string type;  // 同种类型卡牌的唯一编号，可以有多张卡牌，用id标识
    public int needEnergy;
    public string name;
    public string skill;
    public string header;  // 标识卡牌种类，工猫、魔术卡、建造卡等
    public CardLevel level;
    public int maxHealth;  // 建筑本身的血量，建筑也可以被攻击
    public int defend;
    public int needBuildingNum;  // 工猫工作速度*回合数>=needBuildingNum时，这个建筑被激活
    public int maxCapacity;
    public int limitEnergy;  // 猫窝特有属性，只能生成能量<=limitEnergy的小猫
}

[Serializable]
public enum CardLevel
{
    GREEN,
    BLUE,
    PURPLE,
    ORANGE
}

[Serializable]
public class CatContainer
{
    List<int> ids;
}

[Serializable]
public class Entity
{
    public int id;  // 这局游戏所有玩家拥有的实体中唯一
    public int maxHealth;
    int health;  // 当前生命值
    public Location loc;
    EquipmentContainer equipments;
    public Owner owner;
    public int maxAction;
    public int nowAction;
    public int seeDistance;
}

[Serializable]
public class Block
{
    public Location loc;
    public Building building;
    //public BlockType type;  // 无法解析匹配枚举
    public String type;
    public Entity entity;
    public bool canSee;
}

[Serializable]
public enum BlockType
{
    STONE,
    GRASS,
    RIVER,
    HILLS,  // 丘陵
    TREES,  // 森林
    MOUNT  // 高山
}

[Serializable]
public class EquipmentContainer
{
    List<int> ids;
}
