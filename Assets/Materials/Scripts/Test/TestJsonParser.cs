using System;
using UnityEngine;

public class TestJsonParser : MonoBehaviour
{
    private void Start()
    {
        JsonParser parser = new JsonParser("start_info");
        parser.Parse();
        // Debug.Log("gamer id: " + parser.GetInfo().self.gamerId);
    }
}
