using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadStaticUI : MonoBehaviour
{
    public TextMeshProUGUI selfName;
    public Slider selfEnergy;
    public Image otherPlayrsInfoIcon;
    public ScrollRect otherPlayersInfo;

    private PlayerInfo selfInfo;
    private bool isOtherListExpand;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
