using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

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

        internal void Send(Transform transform)
        {
            throw new NotImplementedException();
        }

        internal void Send(byte[] data)
        {
            pool.Return(data);
        }
    }
}
