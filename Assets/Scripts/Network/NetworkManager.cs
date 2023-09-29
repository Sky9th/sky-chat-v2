using UnityEngine;
using System;
using Sky9th.Network.Transport;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace Sky9th.Network
{
    public class NetworkManager : MonoBehaviour
    {

        public string address;
        public int port;
        public int rate = 30;
        public NetworkTransport networkTransport;
        public GameObject playerPerfab;
        public NetworkDataFacotry networkDataFactory;

        public NetworkWriter networkWriter;
        public NetworkReader networkReader;
        public NetworkMessage<ArraySegment<byte>> networkMessage;
        public NetworkPool<ArraySegment<byte>> sendPool = new();
        public NetworkPool<ArraySegment<byte>> receivePool = new();

        private GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            networkWriter = new(sendPool);
            networkDataFactory = new();
            networkReader = new(networkDataFactory);
            networkMessage = new NetworkMessage<ArraySegment<byte>>(networkTransport, networkReader, networkWriter);
            networkTransport.Connect(address, port);

            //networkTransport.OnConnectedEvent += OnConnected;
            networkTransport.OnReceiveEvent += OnReceive;

            StartCoroutine(SendMsg());
        }

        // Update is called once per frame
        void Update()
        {
            if (networkTransport.readyState)
            {
                if (player == null)
                {
                    player = networkDataFactory.PlayerRespawn(playerPerfab);
                } else
                {
                    networkMessage.Read();
                    networkDataFactory.HandlerNetworkData();
                }
            }
        }

        IEnumerator SendMsg ()
        {
            while(true)
            {
                if (networkTransport.readyState)
                {
                    networkMessage.EnqueueSend(sendPool);
                    networkMessage.Send();
                }
                yield return new WaitForSeconds((float)1 / 1000 * rate);
            }
        }

        void OnReceive(byte[] bytes)
        {
            try
            {
                networkMessage.EnqueueReceive(bytes, receivePool);
                networkMessage.Read();
            } catch (Exception e)
            {
                Debug.Log(e.StackTrace);
            }
        }
    }
}
