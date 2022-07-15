using Common.Task.Base;

namespace Common.Task.LoginTask
{
    public class C2S_LoginTask : TaskBase
    {
        public int nCharCode { get; set; } = 0;
        public string pCharName { get; set; } = "";

        public C2S_LoginTask() : base()
        {
        }
    }
}