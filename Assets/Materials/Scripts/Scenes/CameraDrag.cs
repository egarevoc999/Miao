using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class NewMonoBehaviour : MonoBehaviour
{
    private Vector3 dragOrigin;
    private bool isDragging = false;
    private float zDepth = 10f;

    [SerializeField]
    private float dragSpeed = 0.5f;

    [SerializeField]
    private Tilemap tilemap;
    private Bounds tilemapBounds;

    // Use this for initialization
    void Start()
    {
        // 获取tilemap及边界
        tilemap = GameObject.Find("background").GetComponent<Tilemap>();
        tilemapBounds = tilemap.localBounds;
    }

    // Update is called once per frame
    void Update()
    {
        HandleDrag();
    }

    private void HandleDrag()
    {
        // 检测鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            // 记录拖动鼠标起点
            Vector3 mousePos = Input.mousePosition;
            // 设置深度 = 相机z轴到平面z轴差
            mousePos.z = zDepth;
            // 转换成unity游戏世界坐标
            dragOrigin = Camera.main.ScreenToWorldPoint(mousePos);

            isDragging = true;
        }

        // 检测鼠标左键释放
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 拖动状态移动相机
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            Vector3 mouseNewPos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 diff = dragOrigin - mouseNewPos;
            diff.z = 0;  // 限制z轴移动

            //Debug.Log("draoOrigin: " + dragOrigin.ToString() + " mouseNewPos: " + mouseNewPos);
            Camera.main.transform.position += diff * dragSpeed;
        }
    }

    private void LateUpdate()
    {        
        // 获取相机位置和旋转
        Vector3 cameraPosition = Camera.main.transform.position;
        Quaternion cameraRotation = Camera.main.transform.rotation;

        // 获取相机的 FOV 和宽高比
        float fov = Camera.main.fieldOfView;
        float aspect = Camera.main.aspect;

        // 计算视野高度和宽度
        float cameraHeight = Mathf.Tan(Mathf.Deg2Rad * fov / 2) * Mathf.Abs(zDepth); // 深度 z 方向决定的高度
        float cameraWidth = cameraHeight * aspect;

        //Debug.Log("cameraHeight: " + cameraHeight + "cameraWidth: " + cameraWidth);

        // 计算相机方向上的偏移量（考虑旋转角度）
        Vector3 forward = Camera.main.transform.forward;  // 相机朝向
        Vector3 upOffset = new Vector3(0, cameraHeight, 0);
        Vector3 upOffset2 = new Vector3(0, cameraHeight*15.0f, 0);  // 15
        Vector3 rightOffset = new Vector3(cameraWidth, 0, 0);
        Vector3 rightOffset2 = new Vector3(cameraWidth*4.0f, 0, 0);  // 4

        // 将视野的上下左右方向转换为世界坐标
        /*Vector3 topEdge = cameraPosition + forward * zDepth + cameraRotation * upOffset;
        Vector3 bottomEdge = cameraPosition + forward * zDepth + cameraRotation * upOffset;
        Vector3 leftEdge = cameraPosition + forward * zDepth - cameraRotation * rightOffset;
        Vector3 rightEdge = cameraPosition + forward * zDepth - cameraRotation * rightOffset;*/

        Vector3 topEdge = cameraPosition + forward * zDepth + cameraRotation * upOffset2;
        Vector3 bottomEdge = cameraPosition + forward * zDepth + cameraRotation * upOffset;  // 下
        Vector3 leftEdge = cameraPosition + forward * zDepth - cameraRotation * rightOffset;  // 左
        Vector3 rightEdge = cameraPosition + forward * zDepth + cameraRotation * rightOffset2;

        // 获取 Tilemap 边界的最小和最大值
        float minX = tilemapBounds.min.x;
        float maxX = tilemapBounds.max.x;
        float minY = tilemapBounds.min.y;
        float maxY = tilemapBounds.max.y;
        //Debug.Log("cameraPosition:" + cameraPosition.ToString() + " forward:" + forward);

        //Debug.Log("minx:" + minX + " maxX:" + maxX + " minY:" + minY + " maxY: " + maxY);

        // 限制 x 方向移动
        if (leftEdge.x < minX)
        {
            cameraPosition.x += minX - leftEdge.x;  // 如果左边缘越界，向右调整相机
        }
        else if (rightEdge.x > maxX)
        {
            cameraPosition.x -= rightEdge.x - maxX;  // 如果右边缘越界，向左调整相机
        }

        // 限制 y 方向移动
        if (bottomEdge.y < minY)
        {
            cameraPosition.y += minY - bottomEdge.y;  // 如果底边缘越界，向上调整相机
        }
        else if (topEdge.y > maxY)
        {
            cameraPosition.y -= topEdge.y - maxY;  // 如果顶边缘越界，向下调整相机
        }
        

        // 更新相机位置
        Camera.main.transform.position = cameraPosition;
    }
}
