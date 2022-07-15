using Client.Model;
using Common.Data;
using Common.Task.LoginTask;
using Common.Task.MessageTask;
using Debug.Logger;

namespace Client
{
    public partial class Client_Form : Form
    {
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UserTextBox.Text.Trim(StrTrimData.TrimStrS)))
            {
                MessageBox.Show("使用者名稱不可以是空的！！！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (bLogin)
            {
                m_SocketClient.bRecvRun = false;
                m_SocketClient.SocketClose();

                m_Character = null;

                LoginBtn.Text = "登入";
                bLogin = false;
            }
            else
            {
                string ip = "127.0.0.1";
                int port = 10201;

                if (string.IsNullOrEmpty(Properties.Settings.Default.IP))
                {
                    ip = Properties.Settings.Default.IP;
                }
                if (Properties.Settings.Default.PORT != 0)
                {
                    port = Properties.Settings.Default.PORT;
                }

                m_SocketClient = new SocketClient(ip, port);
                m_SocketClient.Connect_Server();

                m_SocketClient.bRecvRun = true;

                recv = new Task(m_SocketClient.Recv_By_Client);
                recv.Start();

                C2S_LoginTask aSend = new C2S_LoginTask();
                aSend.nCharCode = nCode;
                aSend.pCharName = UserTextBox.Text;
                m_SocketClient.Send_Client(aSend);

                LoginBtn.Text = "離線";
                bLogin = true;
            }
        }

        private void OutputMessageBtn_Click(object sender, EventArgs e)
        {
            if (m_SocketClient == null)
            {
                MessageBox.Show("請先登入！！！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(OutputMessageBox.Text.Trim(StrTrimData.TrimStrS)))
            {
                return;
            }

            C2S_MessageTask aSend = new C2S_MessageTask();
            aSend.m_CharCode = m_Character.m_CharCode;
            aSend.pMessage = OutputMessageBox.Text;
            m_SocketClient.Send_Client(aSend);
        }
    }
}