using Sky9th.Network;
using Sky9th.Protobuf;
using System;
using System.Collections.Generic;
using UnityEngine;

delegate void NetowrkDataHandler(byte[] data, string method);

public class NetworkDataFacotry
{
    public Dictionary<Guid, GameObject> respawnGameObjDic = new();

    public Queue<Dictionary<string, object>> data = new();

    private NetowrkDataHandler networkDataHandler;

    public void HandlerNetworkData()
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
                Message message = Message.Parser.ParseFrom(data);
                switch (message.Type)
                {
                    case "Respawn":
                        Respawn respawn = Respawn.Parser.ParseFrom(data);
                        ParseRespawn(respawn);
                        break;
                    default:
                        networkDataHandler.Invoke(data, method);
                        break;

                }
            }
        }
    }

    public void ParseRespawn(Respawn respawn)
    {
        PrefabList prefabList = GameObject.FindAnyObjectByType<PrefabList>();
        GameObject prefab = null;
        switch (respawn.RespawnType)
        {
            case RespawnType.Player:
                prefab = prefabList.player;
                break;
        }
        if (prefab != null)
        {
            Guid guid = Guid.Parse(respawn.NetworkID);
            if (!respawnGameObjDic.ContainsKey(guid))
            {
                Debug.Log("respawn other plyaer:" + guid);
                GameObject gameObject = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
                gameObject.GetComponent<NetworkObject>().networkIdentify = guid;
                respawnGameObjDic.Add(guid, gameObject);
                networkDataHandler += gameObject.NetworkDataHandler;
            }
        }
    }

    public GameObject PlayerRespawn (GameObject playerPerfab)
    {
        Guid guid = Guid.NewGuid();
        Debug.Log("respawn plyaer:" + guid);
        GameObject player = GameObject.Instantiate(playerPerfab, Vector3.zero, Quaternion.identity);
        // 可根据需要对对象进行进一步操作，例如设置位置、旋转或其他属性
        player.transform.position = new Vector3(0, 0, 0);
        player.GetComponent<NetworkObject>().networkIdentify = guid;
        player.GetComponent<NetworkObject>().isLocalPlayer = true;
        respawnGameObjDic.Add(guid, player);
        networkDataHandler += player.NetworkDataHandler;
        return player;
    }
}