using Client.Model;
using Common.Data;
using Debug.Logger;

namespace Client
{
    public partial class Client_Form : Form
    {
        public static Client_Form m_Self { get; set; }

        public Client_Form()
        {
            m_Self = this;

            Logger.Instance.Notice("------Client Start------");

            InitializeComponent();
        }

        int nCode = 0;
        Character m_Character;

        bool bLogin = false;
        SocketClient m_SocketClient;
        Task recv;
    }
}