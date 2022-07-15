using Client.Model;
using Common.Data;
using Common.Task.LoginTask;
using Common.Task.MessageTask;
using Debug.Logger;

namespace Client
{
    public partial class Client_Form : Form
    {
        public void LoginRecv(S2C_LoginTask aRecv)
        {
            if (aRecv == null) return;
            if (aRecv.m_Character == null) return;

            m_Character = aRecv.m_Character;
            nCode = aRecv.m_Character.m_CharCode;

            Logger.Instance.General("接收到登入：Code_{0}  Name_{1}  EndPoint_{2}", m_Character.m_CharCode, m_Character.pUserName, m_Character.pEndPoint);
        }

        public void MessageRecv(S2C_MessageTask aRecv)
        {
            if (aRecv == null) return;
            if (string.IsNullOrEmpty(aRecv.pMessage)) return;

            if (aRecv.m_Char.m_CharCode == m_Character.m_CharCode)
            {
                OutputMessageBox.Text = "";
            }

            if (string.IsNullOrEmpty(MessageTextBox.Text))
            {
                MessageTextBox.Text += aRecv.m_Char.pUserName + "：" + aRecv.pMessage;
            }
            else
            {
                MessageTextBox.Text += Environment.NewLine + aRecv.m_Char.pUserName + "：" + aRecv.pMessage;
            }
        }
    }
}