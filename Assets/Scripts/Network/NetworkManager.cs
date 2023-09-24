using UnityEngine;
using System;
using Sky9th.Network.Transport;
using System.Text;
using System.Collections;

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
                    player = Instantiate(playerPerfab, Vector3.zero, Quaternion.identity);
                    // 可根据需要对对象进行进一步操作，例如设置位置、旋转或其他属性
                    player.transform.position = new Vector3(0, 0, 0);
                } else
                {
                    networkMessage.Read();
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
