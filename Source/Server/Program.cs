using Debug.Logger;
using Server.Core;

namespace Server
{
	internal class Program
	{
		static void ServerError(Exception e)
		{
			Logger.Instance.Error(e);
			File.AppendAllLines(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".txt", new[] { e.ToString() });
		}

		static ServerSocket m_Server;

		static void Main()
		{
			Logger.Instance.Notice("------- Server On -------");
			try
			{
				m_Server = ServerSocket.Instance();
			}
			catch (Exception e)
			{
				ServerError(e);
				return;
			}

			var ServerRun = new Thread(m_Server.Init);
			ServerRun.Start();
		}
	}
}