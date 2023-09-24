

using HybridWebSocket;
using System;
using System.Text;
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

            OnConnecting(uri, port);

            ws.OnOpen += () => {
                readyState = true;
                OnConnected();
            }; 

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
            //Debug.Log("Send data: " + bytes);
            ws.Send(bytes);
        }

    }

}