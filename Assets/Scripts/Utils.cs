using Sky9th.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static Guid playerNetworkId ;

    public static void SetPlayerNetworkId (Guid guid)
    {
        playerNetworkId = guid;
    }

    public static bool IsLocalPlayer ()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        return gameObject.GetComponent<NetworkObject>().NetworkIdentify == playerNetworkId;
    }

}