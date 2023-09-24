using Sky9th.Network;
using Sky9th.Protobuf;
using System;
using System.Collections.Generic;
using UnityEngine;
public class NetworkDataFacotry : MonoBehaviour
{
    public Dictionary<Guid, GameObject> playerDic = new();

    public GameObject playerPerfab;

    public Queue<Dictionary<string, object>> data = new();

    private void Update()
    {
        data.TryDequeue(out Dictionary<string, object> valuePairs);
        if (valuePairs != null && valuePairs.Count > 0)
        {
            object methodObj; object dataObj;
            valuePairs.TryGetValue("method", out methodObj);
            valuePairs.TryGetValue("data", out dataObj);

            byte[] data = (byte[])dataObj;
            string method = (string)methodObj;

            if(method != null && data != null && data.Length > 0)
            {
                CallParse(method, data);
            }
        }
    }

    internal void CallParse(string typeStr, byte[] dataBytes)
    {
        switch(typeStr)
        {
            case "ParsePlayerInfo":
                ParsePlayerInfo(dataBytes);
                break;
        }
    }

    public void ParsePlayerInfo(byte[] msg)
    {
        PlayerInfo data = PlayerInfo.Parser.ParseFrom(msg);
        Guid networkId = Guid.Parse(data.NetworkID);
        if (!playerDic.ContainsKey(networkId))
        {
            Debug.Log("Create Object for:" + networkId);
            GameObject player = Instantiate(playerPerfab, Vector3.zero, Quaternion.identity);
            // 可根据需要对对象进行进一步操作，例如设置位置、旋转或其他属性
            player.transform.position = new Vector3(0, 0, 0);
            player.GetComponent<NetworkObject>().NetworkIdentify = networkId;
            playerDic.Add(networkId, player);
        }
    }
}