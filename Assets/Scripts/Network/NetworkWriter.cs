using Google.Protobuf;
using Sky9th.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sky9th.Network
{
    public class NetworkWriter
    {

        private NetworkPool<ArraySegment<byte>> pool;

        public NetworkWriter(NetworkPool<ArraySegment<byte>> sendPool)
        {
            pool = sendPool;
        }

        public void Send (string msg)
        {
            pool.Return(Encoding.UTF8.GetBytes(msg));
        }

        internal void Send(byte[] data)
        {
            pool.Return(data);
        }

        internal void Send (object data)
        {
            string type = data.GetType().Name;
            string typeName = type.PadLeft(12, '0');
            byte[] typeBytes = Encoding.UTF8.GetBytes(typeName);
            byte[] dataBytes = ((Google.Protobuf.IMessage)data).ToByteArray();
            byte[] bytes = new byte[typeBytes.Length + dataBytes.Length];

            Buffer.BlockCopy(typeBytes, 0, bytes, 0, typeBytes.Length);
            Buffer.BlockCopy(dataBytes, 0, bytes, 12, dataBytes.Length);
            pool.Return(bytes);
        }
    }
}
