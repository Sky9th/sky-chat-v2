using System;

namespace Sky9th.Network
{

    public interface NetWorkTransportInterface
    {

        public void Connect(Uri uri, int port);

        public void Send(byte[] bytes);

        public void Close();

        public void OnConnected();
        public void OnSend();
        public void OnReceive(byte[] bytes);
        public  void OnClose<T>(T msg);

        public void OnError<T>(T msg);

    }
}
