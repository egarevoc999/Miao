using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class testpost : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("start");
        StartCoroutine(PostA());
    }

    [System.Serializable]
    class LoginInfo
    {
        public string phone;
        public int code;
    }

    IEnumerator PostA()
    {
        LoginInfo info = new LoginInfo()
        {
            phone = "15960853997",
            code = 0
        };
        string jsonD = JsonUtility.ToJson(info);
        byte[] body = System.Text.Encoding.UTF8.GetBytes(jsonD);
        using (UnityWebRequest w = new UnityWebRequest("http://192.168.1.158:8080/cwall/login", "POST"))
        {
            w.uploadHandler = new UploadHandlerRaw(body);
            w.downloadHandler = new DownloadHandlerBuffer();
            w.SetRequestHeader("Content-Type", "application/json");
            yield return w.SendWebRequest();
            if (w.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(w.error);
            } else
            {
                Debug.LogError(w.downloadHandler.text);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
