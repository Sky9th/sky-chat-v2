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
        public NetworkTransport netWorkTransport;
        public GameObject playerPerfab;

        public NetworkWriter networkWriter;
        public NetworkMessage<ArraySegment<byte>> networkMessage;
        public NetworkPool<ArraySegment<byte>> sendPool = new();

        // Start is called before the first frame update
        void Start()
        {
            networkWriter = new(sendPool);
            netWorkTransport.Connect(address, port);
            netWorkTransport.onConnected += () =>
            {
                networkMessage = new NetworkMessage<ArraySegment<byte>> (netWorkTransport);
            };
            StartCoroutine(SendMsg());

        }

        // Update is called once per frame
        void Update()
        {
        }

        IEnumerator SendMsg ()
        {
            while(true)
            {
                if (netWorkTransport.readyState)
                {
                    //Debug.Log(Time.time + ": send msg");
                    //networkWriter.Send("This is test1");
                    //networkWriter.Send("This is test2");
                    networkMessage.EnqueueSend(sendPool);
                    networkMessage.Send();
                }
                yield return new WaitForSeconds((float)1 / 1000 * rate);
            }
        }
    }
}
