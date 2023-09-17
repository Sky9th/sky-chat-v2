

using HybridWebSocket;
using System;
using UnityEngine;

namespace Sky9th.Network.Transport
{
    public class WebSocket : NetworkTransport
    {

        private HybridWebSocket.WebSocket ws;

        public override void Connect(string uri, int port)
        {
            this.uri = uri;
            this.port = port;

            Debug.Log("connecting to ws://" + uri + ":" + port);
            ws = WebSocketFactory.CreateInstance("ws://" + uri + ":" + port);

            ws.OnOpen += OnConnected;
            ws.OnClose += OnClose;
            ws.OnError += OnError;
            ws.OnMessage += OnReceive;

            ws.Connect();
        }

        public override void Close()
        {
            Debug.Log("Close to ws://" + uri + ":" + port);
            ws.Close();
        }

        public override void Send(byte[] bytes)
        {
            Debug.Log("Send data: " + bytes);
            OnSend();
            ws.Send(bytes);
        }

        public override void OnConnected()
        {
            Debug.Log("Connect to ws://" + uri + ":" + port + " success");
            readyState = true;
            if (onConnected != null)
            {
                onConnected();
            }
        }

        public override void OnReceive(byte[] bytes)
        {
            if (onReceive != null)
            {
                onReceive();
            }
        }

        public override void OnSend()
        {
            if (onSend != null)
            {
                onSend();
            }
        }

        public override void OnClose<T>(T msg)
        {
            if (onClose != null)
            {
                onClose();
            }
        }

        public override void OnError<T>(T msg)
        {
            Debug.Log("Error with: " + msg);
            if (onError != null)
            {
                onError();
            }
        }
    }

}