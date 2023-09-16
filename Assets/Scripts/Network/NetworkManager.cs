using UnityEngine;
using System;
using Sky9th.Network.Transport;

namespace Sky9th.Network
{
    public class NetworkManager : MonoBehaviour
    {

        public string address;

        public int port;

        public int rate = 30;

        public NetworkTransport netWorkTransport;

        // Start is called before the first frame update
        void Start()
        {
            netWorkTransport.Connect(address, port);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
