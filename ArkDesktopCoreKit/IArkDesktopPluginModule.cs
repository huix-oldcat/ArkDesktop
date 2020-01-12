using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkDesktop.CoreKit
{
    public interface IArkDesktopPluginModule//V3
    {
        string Name { get; }
        Guid Guid { get; }
        bool Disposed { get; }
        string Description { get; }
        int Version { get; }
        void MainThread(ResourceManager resources, InstanceHelper instanceHelper);
        void RequestDispose();
    }
}
