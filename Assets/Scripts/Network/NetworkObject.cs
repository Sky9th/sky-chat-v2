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

        public Guid networkIdentify;

        public bool isLocalPlayer = false;

        public Protobuf.Transform nextTransform = null;

        // Start is called before the first frame update
        void Start()
        {
            networkManager = GameObject.FindFirstObjectByType<NetworkManager>();
            networkDataFacotry = networkManager.networkDataFactory;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (networkIdentify != null && isLocalPlayer)
            {
                if (networkWriter == null)
                {
                    networkWriter = networkManager.networkWriter;
                }
                else
                {
                    Debug.Log(networkIdentify.ToString());
                    PlayerInfo playerInfo = new()
                    {
                        NetworkID = networkIdentify.ToString(),
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
            } else
            {
                if (nextTransform != null && (transform.position.x != nextTransform.X || transform.position.y != nextTransform.Y))
                {
                    transform.position = new Vector3((float)nextTransform.X, (float)nextTransform.Y, (float)nextTransform.Z);
                }
            }
        }
    }
}
