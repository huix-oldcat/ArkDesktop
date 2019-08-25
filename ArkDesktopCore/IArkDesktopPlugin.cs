using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkDesktop
{
    public interface IArkDesktopPlugin
    {
        string Name { get; }
        void MainThread(object coreInst);
        void RequestDispose();
    }
}
