using System;
using UnityEngine;

namespace Sky9th.Network.Transport
{

    public abstract class NetworkTransport : MonoBehaviour
    {
        protected string uri;
        protected int port;

        public abstract void Close();

        public abstract void Connect(string uri, int port);

        public abstract void OnClose<T>(T msg);

        public abstract void OnConnected();

        public abstract void OnError<T>(T msg);

        public abstract void OnReceive(byte[] bytes);

        public abstract void OnSend();

        public abstract void Send(byte[] bytes);
    }
}
