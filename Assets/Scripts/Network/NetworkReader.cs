using ProtoBuf;
using Sky9th.Protobuf;
using System;
using UnityEngine;

namespace Sky9th.Network
{
    public class NetworkReader
    {

        private NetworkPool<ArraySegment<byte>> pool;

        public NetworkReader(NetworkPool<ArraySegment<byte>> sendPool)
        {
            pool = sendPool;
        }

        public void Read(byte[] msg)
        {

            // 反序列化字节数组为Protobuf对象
            /*Message obj = Deserialize<Message>(msg);
            string type = obj.Type;
            Debug.Log(type);*/
        }

        private T Deserialize<T>(byte[] data)
        {
            using (var stream = new System.IO.MemoryStream(data))
            {
                return Serializer.Deserialize<T>(stream);
            }
        }
    }
}
