using Google.Protobuf;
using Sky9th.Protobuf;
using System;
using UnityEngine;

namespace Sky9th.Network
{
    public class NetworkObject : MonoBehaviour
    {

        protected NetworkManager networkManager;
        protected NetworkWriter networkWriter;

        [SerializeField]
        protected Guid networkIdentify;

        private bool test = true;

        // Start is called before the first frame update
        void Start()
        {
            networkManager = GameObject.FindFirstObjectByType<NetworkManager>();

            networkIdentify = Guid.NewGuid();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (test)
            {
                if (networkWriter == null)
                {
                    networkWriter = networkManager.networkWriter;
                }
                else
                {
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
                    byte[] data = ((Google.Protobuf.IMessage)playerInfo).ToByteArray();
                    networkWriter.Send(data);
                }

            }
        }
    }
}
