using Common.Data;
using Common.Task.Base;

namespace Common.Task.MessageTask
{
    public class S2C_LoginTask : TaskBase
    {
        public Character m_Character { get; set; } = null;

        public S2C_LoginTask() : base()
        {
        }
    }
}