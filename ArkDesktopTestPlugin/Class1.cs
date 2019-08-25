using System.Threading;
using System.Windows.Forms;

namespace ArkDesktop
{
    public class Plugin : IArkDesktopPlugin
    {
        private bool closing = false;
        private TestConfigControl cc = new TestConfigControl();
        public string Name
        {
            get
            {
                return "ArkDesktop.TestPlugin";
            }
        }

        public void MainThread(object coreInst)
        {
            Core core = (Core)coreInst;
            core.AddControl("TestPlugin", cc);
            cc.requireClose = () => closing = true;
            while (!closing) ;
            core.RemoveControl("TestPlugin");
            cc.Invoke((MethodInvoker)(() => cc.Dispose()));
        }

        public void RequestDispose()
        {
            
        }
    }
}
