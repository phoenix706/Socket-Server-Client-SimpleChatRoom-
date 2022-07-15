using Common.Data;
using Common.Task.Base;
using Common.Task.LoginTask;
using Common.Task.MessageTask;
using Debug.Logger;
using System.Collections;
using System.Net;
using System.Net.Sockets;

namespace Server.Core
{
	class ServerSocket
	{
        private static ServerSocket m_SocketServer;
        public static ServerSocket Instance()
        {
            if (m_SocketServer == null)
            {
                m_SocketServer = new ServerSocket();
            }
            return m_SocketServer;
        }

        private Socket m_Socket;
        private IPAddress m_IPAddress;
        private int nPort;

        private Dictionary<int, Character> m_CharS = new Dictionary<int, Character>();

        public void Init()
        {
            m_IPAddress = IPAddress.Parse("127.0.0.1");
            nPort = 10201;

            CreateSocket();
            BindAndListen();
            WaitClientConnection();
        }

        private void CreateSocket()
        {
            m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void BindAndListen()
        {
            m_Socket.Bind(new IPEndPoint(m_IPAddress, nPort));
            m_Socket.Listen(100);
        }

        private Dictionary<int, Socket> m_ConnectSocketList = new Dictionary<int, Socket>();
        private Dictionary<int, List<Task>> recvThreadList = new Dictionary<int, List<Task>>();

        private void WaitClientConnection()
        {
            int index = 1;
            while (true)
            {
                Socket ClientSocket = m_Socket.Accept();
                if (ClientSocket != null)
                {
                    Logger.Instance.Notice("{0}連線成功！", ClientSocket.RemoteEndPoint);
                    m_ConnectSocketList.Add(index, ClientSocket);

                    Task recv = new Task(() => { Recv(new ArrayList { index, ClientSocket }); });
                    recv.Start();
                    recvThreadList.Add(index, new List<Task> { recv });
                    index++;
                }
            }
        }

        public void Recv(object client_socket)
        {
            ArrayList arraylist = client_socket as ArrayList;
            int index = (int)arraylist[0];
            Socket clientsocket = arraylist[1] as Socket;
            while (true)
            {
                try
                {
                    byte[] getrecv = new byte[2048];
                    int count = clientsocket.Receive(getrecv);
                    if (count <= 0) break;

                    var typeStr = TcpEndPoint.GetType<TaskBase>(getrecv, count);

                    Server_Received(typeStr, getrecv, count, clientsocket, index);
                }
                catch (Exception)
                {
                    Logger.Instance.General("代號為：{0}的客戶端已經離去！", index);
                    recvThreadList.Remove(index);
                    break;
                }
            }
        }

        private void Send<V>(V value, Socket aSocket, int nIndex) where V : TaskBase
        {
            if (aSocket == null) return;

            try
            {
                byte[] ChangebyteS = TcpEndPoint.Serialize<V>(value);
                aSocket.Send(ChangebyteS);
            }
            catch (Exception)
            {
                //Logger.Instance.General("代號為：{0}的客戶端已經離去！訊息傳送失敗！", nIndex);
                recvThreadList.Remove(nIndex);
            }
        }

        Random m_Random = new Random(521221);
        public void Server_Received(string typeStr, byte[] data, int count, Socket aSocket, int nIndex)
        {
            try
            {
                switch (typeStr)
                {
                    case "C2S_MessageTask":
                        {
                            var aObj = TcpEndPoint.DeSerialize<C2S_MessageTask>(data, count);
                            if (aObj == null) return;

                            S2C_MessageTask aSend = new S2C_MessageTask();
                            aSend.m_Char = m_CharS.GetValueOrDefault(aObj.m_CharCode, null);
                            aSend.pMessage = aObj.pMessage;

                            foreach (var aClientSocket in m_ConnectSocketList)
                            {
                                Send(aSend, aClientSocket.Value, aClientSocket.Key);
                            }

                            Console.WriteLine("接收到來自 {0} 的訊息為：{1}", aObj.m_CharCode, aObj.pMessage);
                        }
                        break;
                    case "C2S_LoginTask":
                        {
                            var aObj = TcpEndPoint.DeSerialize<C2S_LoginTask>(data, count);
                            if (aObj == null) return;

                            Character aChar = new Character();
                            aChar.m_CharCode = aObj.nCharCode == 0 ? m_Random.Next(1,999) : aObj.nCharCode;
                            aChar.pUserName = aObj.pCharName;
                            aChar.pEndPoint = aSocket.RemoteEndPoint.ToString();

                            if (m_CharS.ContainsKey(aChar.m_CharCode))
                                m_CharS[aChar.m_CharCode] = aChar;
                            else
                                m_CharS.Add(aChar.m_CharCode, aChar);

                            var aSend = new S2C_LoginTask();
                            aSend.m_Character = aChar;
                            Send(aSend, aSocket, nIndex);

                            Console.WriteLine("接收到登入：Code_{0}  Name_{1}  EndPoint_{2}", aChar.m_CharCode, aChar.pUserName, aChar.pEndPoint);
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
