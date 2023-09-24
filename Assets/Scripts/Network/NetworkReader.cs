using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Sky9th.Network
{
    public class NetworkReader
    {
        private NetworkDataFacotry networkDataFacotry;

        public NetworkReader(NetworkDataFacotry networkDataFacotry)
        {
            this.networkDataFacotry = networkDataFacotry;
        }

        public void Read(byte[] msg)
        {
            if (msg.Length > 0)
            {
                byte[] typeBytes = new byte[12];
                Buffer.BlockCopy(msg, 0, typeBytes, 0, typeBytes.Length);
                string typeStr = Encoding.UTF8.GetString(typeBytes).Replace("0", "");
                if (typeStr != null)
                {
                    byte[] dataBytes = new byte[msg.Length - typeBytes.Length];
                    Buffer.BlockCopy(msg, typeBytes.Length, dataBytes, 0, dataBytes.Length);

                    Dictionary<string, object> keyValuePairs = new()
                    {
                        { "method", "Parse" + typeStr },
                        { "data", dataBytes }
                    };
                    networkDataFacotry.data.Enqueue(keyValuePairs);
                }
            }
        }
    }
}
