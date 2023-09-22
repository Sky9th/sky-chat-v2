using System;
using UnityEngine;

namespace Sky9th.Network.Transport
{

    public delegate void OnConnectiongEventHandler(string uri, int port);
    public delegate void OnConnectedEventHandler(string uri, int port);
    public delegate void OnReceiveEventHandler(byte[] bytes);
    public delegate void OnSendEventHandler(byte[] bytes);
    public delegate void OnErrorEventHandler();
    public delegate void OnCloseEventHandler();


    public abstract class NetworkTransport : MonoBehaviour
    {
        protected string uri;
        protected int port;

        public event OnConnectiongEventHandler OnConnectingEvent;
        public event OnConnectedEventHandler OnConnectedEvent;
        public event OnReceiveEventHandler OnReceiveEvent;
        public event OnSendEventHandler OnSendEvent;
        public event OnErrorEventHandler OnErrorEvent;
        public event OnCloseEventHandler OnCloseEvent;

        public bool readyState = false;

        public abstract void Connect(string uri, int port);

        public abstract void Send(byte[] bytes);

        public abstract void Close();

        public virtual void OnConnecting(string uri, int port)
        {
            Debug.Log("OnConnecting");
            OnConnectingEvent?.Invoke(uri, port);
        }

        public virtual void OnConnected()
        {
            Debug.Log("OnConnected");
            OnConnectedEvent?.Invoke(uri, port);
        }

        public virtual void OnReceive(byte[] bytes)
        {
            Debug.Log("OnReceive");
            OnConnectingEvent?.Invoke(uri, port);
        }

        public virtual void OnClose<T>(T closeCode)
        {
            Debug.Log("OnClose");
            OnCloseEvent?.Invoke();
        }

        public virtual void OnError<T>(T msg)
        {
            Debug.Log("OnError");
            OnErrorEvent?.Invoke();
        }
    }
}
