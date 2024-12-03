using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class LoadStaticUI : MonoBehaviour
{
    public TextMeshProUGUI selfName;
    public Slider selfEnergy;
    public Image otherPlayrsInfoIcon;
    public ScrollRect otherPlayersInfo;

    private PlayerInfo selfInfo;
    private bool isOtherListExpand;

    public GameObject otherPlayerItem;  // 玩家预设体
    public Transform contentTransform;  // 滚动条的content
    public Sprite defaultAvatar;

    private List<PlayerInfo> othersInfo;

    // Use this for initialization
    void Start()
    {
        Debug.Log("[LoadStaticUI] start");

        // 加载JSON文件
        JsonParser parser = new JsonParser("start_info");
        parser.Parse();
        Debug.Log("[LoadStaticUI] gamer id: " + parser.GetInfo().self.gamerId + ", name: " + parser.GetInfo().self.name + ", id: " + parser.GetInfo().self.id);

        // 获取并更新self玩家信息
        selfInfo = parser.GetInfo().self;
        UpdateSelfInfo();

        // 禁用slider的交互功能
        this.selfEnergy.interactable = false;

        // 默认不显示其他玩家信息
        this.otherPlayersInfo.gameObject.SetActive(false);
        // 监听按钮事件
        this.otherPlayrsInfoIcon.GetComponent<Button>().onClick.AddListener(ToggleList);

        // 获取并更新others玩家信息
        othersInfo = parser.GetInfo().others;
        UpdateOthersInfo();
    }

    private void ToggleList()
    {
        if (isOtherListExpand)
        {
            Debug.Log("Contract other players info view");
            this.otherPlayersInfo.gameObject.SetActive(false);
        } else
        {
            Debug.Log("Expand other players info view");
            this.otherPlayersInfo.gameObject.SetActive(true);
        }
        isOtherListExpand = !isOtherListExpand;
    }

    private void UpdateSelfInfo()
    {
        this.selfName.text = selfInfo.name;

        int ev = Mathf.Clamp(selfInfo.energyNum, (int)selfEnergy.minValue, (int)selfEnergy.maxValue);
        this.selfEnergy.value = ev;
    }

    private void UpdateOthersInfo()
    {
        foreach(var player in othersInfo)
        {
            Debug.Log("add other player");
            CreatePlayerItem(player);
        }
    }

    private void CreatePlayerItem(PlayerInfo info)
    {
        // 实例化玩家
        GameObject item = Instantiate(otherPlayerItem, contentTransform);

        // 更新玩家名称子组件
        TextMeshProUGUI name = item.GetComponentInChildren<TextMeshProUGUI>();
        name.text = info.name;
        Debug.Log("other player's name is " + name.text);

        // 更新玩家头像子组件
        Image avatar = item.GetComponentInChildren<Image>();
        avatar.sprite = defaultAvatar;
        // TODO 更新其他玩家头像
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
