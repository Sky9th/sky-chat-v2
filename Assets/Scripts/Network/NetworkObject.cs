using Google.Protobuf;
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

        [SerializeField]
        public Guid NetworkIdentify;

        public string NetworkIdentifyStr;

        // Start is called before the first frame update
        void Start()
        {
            networkManager = GameObject.FindFirstObjectByType<NetworkManager>();
            networkDataFacotry = networkManager.networkDataFactory;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (networkWriter == null)
            {
                networkWriter = networkManager.networkWriter;
            }
            else
            {
                PlayerInfo playerInfo = new()
                {
                    NetworkID = NetworkIdentify.ToString(),
                    Type = "PlayerInfo",
                    Transform = new Protobuf.Transform()
                    {
                        X = transform.position.x,
                        Y = transform.position.y,
                        Z = transform.position.z
                    }
                };
                networkWriter.Send(playerInfo);
            }
        }
    }
}
