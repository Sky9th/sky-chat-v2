using Sky9th.Network.Transport;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Sky9th.Network
{
    public class NetworkMessage <T>
    {
        protected ConcurrentQueue<byte[]> sendQueue = new();
        protected ConcurrentQueue<byte[]> receiveQueue = new();

        protected NetworkTransport networkTransport;

        protected int maxMessageSize = 60000;

        public NetworkMessage (NetworkTransport networkTransport)
        {
            this.networkTransport = networkTransport;
        }

        public void EnqueueSend(NetworkPool<T> pool)
        {
            if (!networkTransport.readyState)  return;

            byte[] bytes = new byte[maxMessageSize];

            int length = 0;

            while(pool.Count > 0)
            {
                //Debug.Log("EnqueueSend in pool");
                T msg = pool.Get();
                byte[] partBytes = ConvertToByte(msg);
                byte[] lengthBytes = Encoding.UTF8.GetBytes(partBytes.Length.ToString().PadLeft(8,'0'));
                length += lengthBytes.Length + partBytes.Length;
                if (length > maxMessageSize)
                {
                    throw new Exception("msg size over all max message size");
                } else
                {
                    Buffer.BlockCopy(lengthBytes, 0, bytes, length - (lengthBytes.Length + partBytes.Length), lengthBytes.Length);
                    Buffer.BlockCopy(partBytes, 0, bytes, length - partBytes.Length, partBytes.Length);
                }
            }

            if (length > 0)
            {
                byte[] totalLengthBytes = Encoding.UTF8.GetBytes(length.ToString().PadLeft(8, '0'));
                byte[] res = new byte[totalLengthBytes.Length + length];

                Buffer.BlockCopy(totalLengthBytes, 0, res, 0, totalLengthBytes.Length);
                Buffer.BlockCopy(bytes, 0, res, totalLengthBytes.Length, length);

                sendQueue.Enqueue(res);
            }

            return;
        }

        public void Send ()
        {
            if (!networkTransport.readyState) return;
            byte[] msg;
            if ( !sendQueue.IsEmpty )
            {
                sendQueue.TryDequeue(out msg);
                Debug.Log("Send msg from sendQueue with length:" + msg.Length);
                networkTransport.Send(msg);
            }
        }

        public void EnqueueReceive(byte[] bytes)
        {
            /*if (!networkTransport.readyState) return;
            string str = Encoding.UTF8.GetString(bytes);
            string splitStr = Encoding.UTF8.GetString(splitBytes);
            string[] strList = str.Split(splitStr);
            for(int i = 0; i < strList.Length - 1; i++ )
            {
                receiveQueue.Enqueue(Encoding.UTF8.GetBytes(strList[i]));
            }*/
        }


        public byte[] ConvertToByte(T msg)
        {
            byte[] bytes;
            if(msg is byte[] _byte)
            {
                bytes = _byte;
            }
            else if (msg is int i)
            {
                bytes = BitConverter.GetBytes(i);
            }
            else if (msg is ArraySegment<byte> arraySegment)
            {
                byte[] _b = new byte[arraySegment.Count];
                Buffer.BlockCopy(arraySegment.Array, arraySegment.Offset, _b, 0, arraySegment.Count);
                bytes = _b;
            }
            else if(msg is string str)
            {
                bytes = Encoding.UTF8.GetBytes(str);
            }
            else
            {
                throw new ArgumentException("unsupport msg type");
            }
            return bytes;
        }

    }
}
