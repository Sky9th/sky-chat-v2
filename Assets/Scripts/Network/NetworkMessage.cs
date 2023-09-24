using Sky9th.Network.Transport;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;
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
        protected NetworkReader networkReader;
        protected NetworkWriter networkWriter;

        protected int maxMessageSize = 60000;

        public NetworkMessage (NetworkTransport networkTransport, NetworkReader networkReader, NetworkWriter networkWriter)
        {
            this.networkTransport = networkTransport;
            this.networkReader = networkReader;
            this.networkWriter = networkWriter;
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

        public void EnqueueReceive(byte[] bytes, NetworkPool<ArraySegment<byte>> networkPool)
        {
            if (bytes.Length > 0)
            {
                byte[] totalLengthBytes = new byte[8];
                Buffer.BlockCopy(bytes, 0, totalLengthBytes, 0, totalLengthBytes.Length);
                string totalLengthStr = Encoding.UTF8.GetString(totalLengthBytes);
                int totalLength = int.Parse(totalLengthStr.TrimStart('0'));

                byte[] dateBytes = new byte[totalLength];
                Buffer.BlockCopy(bytes, totalLengthBytes.Length, dateBytes, 0, totalLength);
                int currentIndex = 0;
                int times = 0;

                while (currentIndex < totalLength)
                {
                    times++;
                    byte[] currentLengthBytes = new byte[8];
                    Buffer.BlockCopy(dateBytes, currentIndex, currentLengthBytes, 0, currentLengthBytes.Length);
                    string currentLengthStr = Encoding.UTF8.GetString(currentLengthBytes);
                    int currentLength = int.Parse(currentLengthStr.TrimStart('0'));
                    currentIndex += currentLengthBytes.Length;

                    byte[] currentDataBytes = new byte[currentLength + 12];
                    Buffer.BlockCopy(dateBytes, currentIndex, currentDataBytes, 0, currentDataBytes.Length);
                    currentIndex += currentDataBytes.Length;
                    receiveQueue.Enqueue(currentDataBytes);
                }
                Debug.Log("parse " + times + " data and length " + totalLength);
            }
        }

        public void Read ()
        {
            byte[] msg;
            if (!receiveQueue.IsEmpty)
            {
                receiveQueue.TryDequeue(out msg);
                Debug.Log("Read msg from receiveQueue with length:" + msg.Length);
                networkReader.Read(msg);
            }
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
