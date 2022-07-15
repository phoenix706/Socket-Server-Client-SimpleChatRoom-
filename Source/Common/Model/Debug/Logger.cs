namespace Debug.Logger
{
	public enum Priority
	{
		General,    //一般
		Notice,     //注意
		Warning,    //警告
		Error,      //錯誤
	}

	public interface ILogTarget
	{
		void Log(Priority priority, string msg);
	}

	public sealed class Logger
	{
		public static readonly Logger Instance = new Logger();

		readonly IDictionary<Priority, bool> Priorities = new Dictionary<Priority, bool>();

		ILogTarget Target;

		Logger()
		{
			Priorities.Add(Priority.General, false);
			Priorities.Add(Priority.Notice, false);
			Priorities.Add(Priority.Warning, false);
			Priorities.Add(Priority.Error, false);
		}

		public void AttachTarget(ILogTarget target)
		{
			EnableAll();
			Target = target;
		}

		public void SetPriority(Priority prio, bool enable) => Priorities[prio] = enable;
		public bool IsEnable(Priority priority) { return Priorities[priority]; }
		public void EnableAll() { foreach (var priority in Priorities.Keys.ToArray()) Priorities[priority] = true; }
		static string AddDateTime(string content) { return string.Format("{0:yyyy/MM/dd HH:mm:ss.fff}: {1}", DateTime.Now, content); }

		public void Log(Priority priority, string format, params object[] args)
		{
			var pStr = AddDateTime((args == null || args.Length == 0) ? format : string.Format(format, args));

			if (Priorities[priority] && Target != null)
				Target.Log(priority, pStr);

            switch (priority)
			{
                #if DEBUG
                case Priority.General:
					Console.WriteLine(pStr);
					break;
				#endif

				case Priority.Notice:
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine(pStr);
					Console.ForegroundColor = ConsoleColor.White;
					break;
                case Priority.Warning:
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine(pStr);
					Console.ForegroundColor = ConsoleColor.White;
					break;
                case Priority.Error:
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(pStr);
					Console.ForegroundColor = ConsoleColor.White;
					break;
            }
        }

		#region General
		public void General(object obj) => Log(Priority.General, "{0}", obj);
		public void General(string format, params object[] args) => Log(Priority.General, format, args);
		#endregion

		#region Notice
		public void Notice(object obj) => Log(Priority.Notice, "{0}", obj);
		public void Notice(string format, params object[] args) => Log(Priority.Notice, format, args);
		#endregion

		#region Warning
		public void Warning(object obj) => Log(Priority.Warning, "{0}", obj);
		public void Warning(string format, params object[] args) => Log(Priority.Warning, format, args);
		#endregion

		#region Error
		public void Error(object obj) => Log(Priority.Error, "{0}", obj);
		public void Error(Exception ex) => Log(Priority.Error, "{0}{1}{2}", ex, Environment.NewLine, ex.StackTrace);
		public void Error(string format, params object[] args) => Log(Priority.Error, format, args);
		#endregion
	}
}