using Common.Task.Base;

namespace Common.Task.MessageTask
{
    public class C2S_MessageTask : TaskBase
    {
        public int m_CharCode { get; set; } = 0;
        public string pMessage { get; set; } = "";

        public C2S_MessageTask() : base()
        {
        }
    }
}