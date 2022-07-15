using Common.Task.Base;
using Common.Task.MessageTask;
using Debug.Logger;
using System.Net;
using System.Net.Sockets;

namespace Client.Model
{
    public class SocketClient
    {
        public SocketClient(string pIP, int nPort)
        {
            this.m_IPAddress = IPAddress.Parse(pIP);
            this.nPort = nPort;
        }

        private Socket m_Client_Socket;
        private IPAddress m_IPAddress;
        private int nPort;
        public Socket Create_Client_Socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect_Server()
        {
            m_Client_Socket = Create_Client_Socket();
            m_Client_Socket.Connect(new IPEndPoint(m_IPAddress, nPort));
        }

        public bool bRecvRun = true;
        public void Recv_By_Client()
        {
            while (bRecvRun)
            {
                byte[] getrecv = new byte[2048];
                int count = m_Client_Socket.Receive(getrecv);
                if (count <= 0) break;

                var typeStr = TcpEndPoint.GetType<TaskBase>(getrecv, count);

                Client_Received(typeStr, getrecv, count);
            }
        }

        public void Send_Client<V>(V value) where V : TaskBase
        {
            if (value == null) return;

            try
            {
                byte[] ChangebyteS = TcpEndPoint.Serialize<V>(value);
                m_Client_Socket.Send(ChangebyteS);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex);
            }
        }

        public void SocketClose()
        {
            m_Client_Socket.Shutdown(SocketShutdown.Both);
            m_Client_Socket.Close();
        }

        public void Client_Received(string typeStr, byte[] data, int count)
        {
            try
            {
                switch (typeStr)
                {
                    case "S2C_MessageTask":
                        {
                            var aObj = TcpEndPoint.DeSerialize<S2C_MessageTask>(data, count);
                            if (aObj == null) return;

                            Client_Form.m_Self.MessageRecv(aObj);

                            //Logger.Instance.General("接收到來自{0}的訊息為：{1}", m_Client_Socket.RemoteEndPoint, aObj.pMessage);
                            Logger.Instance.General("接收到來自 {0} 的訊息為：{1}", aObj.m_Char.pUserName, aObj.pMessage);
                        }
                        break;
                    case "S2C_LoginTask":
                        {
                            var aObj = TcpEndPoint.DeSerialize<S2C_LoginTask>(data, count);
                            if (aObj == null) return;

                            Client_Form.m_Self.LoginRecv(aObj);

                            Logger.Instance.General("登入：Code_{0}  Name_{1}", aObj.m_Character.m_CharCode, aObj.m_Character.pUserName);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exception)
            {
                Logger.Instance.Error(exception);
            }
        }
    }
}