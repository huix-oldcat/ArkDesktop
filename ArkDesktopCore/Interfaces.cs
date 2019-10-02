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
        string Description { get; }
        int Version { get; }
    }

    public interface IArkDesktopLaunchable : IArkDesktopPlugin
    {
        void MainThread(object coreInst);
        void RequestDispose();
    }

    public interface IArkDesktopV2 : IArkDesktopLaunchable
    { }
}
