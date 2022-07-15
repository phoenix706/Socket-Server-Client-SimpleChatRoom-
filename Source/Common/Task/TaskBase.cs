using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace Common.Task.Base
{
    public class TaskBase
    {
		public string typeName { get; set; }

		public TaskBase()
		{
			typeName = GetType().Name;
		}
	}

    public class TcpEndPoint
	{
		public static byte[] Serialize<V>(V value) where V : TaskBase
		{
			var JsonStr = JsonSerializer.Serialize<V>(value);
			byte[] buffer = Encoding.UTF8.GetBytes(JsonStr);
			return buffer;
		}

		public static string GetType<V>(byte[] data, int count) where V : TaskBase
		{
			string str = Encoding.UTF8.GetString(data, 0, count);
			var getType = JsonSerializer.Deserialize<V>(str);
			return getType.typeName;
		}

		public static V DeSerialize<V>(byte[] data, int count) where V : TaskBase
		{
			string str = Encoding.UTF8.GetString(data, 0, count);
			var recv = JsonSerializer.Deserialize<V>(str);

			return recv;
		}
	}
}