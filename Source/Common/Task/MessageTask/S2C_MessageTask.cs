using Common.Data;
using Common.Task.Base;

namespace Common.Task.MessageTask
{
    public partial class S2C_MessageTask : TaskBase
    {
        public Character m_Char { get; set; } = null;
        public string pMessage { get; set; } = "";
        public DateTime m_MessageDateTime { get; set; } = DateTime.Now;

        public S2C_MessageTask() : base()
        {
        }
    }
}