using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Sky9th.Protobuf;
using System;
using System.Text;
using UnityEngine;

namespace Sky9th.Network
{

    public class NetworkObject : MonoBehaviour
    {

        protected NetworkManager networkManager;
        protected NetworkDataFacotry networkDataFacotry;
        protected NetworkWriter networkWriter;

        protected NetworkPool<object> networkPool;

        public Guid networkIdentify;

        public bool isLocalPlayer = false;

        public void Inject()
        {
            networkManager = GameObject.FindFirstObjectByType<NetworkManager>();
            networkWriter = networkManager.networkWriter;
            networkDataFacotry = networkManager.networkDataFactory;
        }

        public virtual void NetworkDataHandler(byte[] msg, string method)
        {
            Debug.Log("NetworkObject NetworkDataHandler:" + msg.Length);
        }
    }
}
