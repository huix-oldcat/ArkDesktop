using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ArkDesktop
{
    public class ArkDesktopStaticPic : IArkDesktopLaunchable
    {
        private bool closed = false;
        private LayeredWindow window;
        private LayeredWindowManager manager;
        private List<Bitmap> bitmaps = new List<Bitmap>();
        private ConfigControl configControl;
        private bool needModify = false;
        private bool allowModify = false;
        private XNamespace ns = "ArkDesktop";
        private Core core;
        private int frameTime;

        public void RequestModifyBitmaps(List<Bitmap> newBitmaps)
        {
            allowModify = false;
            needModify = true;
            while (!allowModify) ;
            bitmaps = newBitmaps;
            needModify = false;
        }

        public string Name
        {
            get
            {
                return "ArkDesktop.StaticPic";
            }
        }

        private void LoadConfig()
        {
            XElement root = core.config.GetElement(ns + "StaticPic");
            frameTime = Convert.ToInt32(root.Element(ns + "FrameTime").Value);
            foreach (XElement element in from e in root.Element(ns + "Frames").Elements() where e.Name == ns + "FrameFile" select e)
            {
                if (File.Exists(element.Value))
                {
                    bitmaps.Add((Bitmap)Image.FromFile(element.Value));
                }
            }
        }

        private void CreateConfig()
        {
            core.config.AppendElement(new XElement(ns + "StaticPic",
                new XElement(ns + "FrameTime", "30"),
                new XElement(ns + "Frames")));
        }

        public void SetFrameList(string[] frames)
        {
            XElement root = core.config.GetElement(ns + "StaticPic").Element(ns + "Frames");
            root.Elements()?.Remove();
            foreach (string frame in frames)
            {
                root.Add(new XElement(ns + "FrameFile", frame));
            }
        }

        public void MainThread(object coreInst)
        {
            core = (Core)coreInst;

            window = core.RequestPlugin("ArkDesktop.LayeredWindow").CreateInstance("ArkDesktop.LayeredWindow") as LayeredWindow;
            manager = core.RequestPlugin("ArkDesktop.LayeredWindowManager").CreateInstance("ArkDesktop.LayeredWindowManager") as LayeredWindowManager;
            manager.window = window;
            manager.config = core.config;
            manager.HelpPositionChange();
            manager.helpZoomChange = true;
            configControl = new ConfigControl();
            configControl.Dock = DockStyle.Fill;
            configControl.makerParent = this;
            configControl.manager = manager;
            core.AddControl("图片播放", configControl);

            if (core.config.GetElement(ns + "StaticPic") == null)
            {
                CreateConfig();
            }
            LoadConfig();

            while (!manager.Ready) ;
            while (!closed)
            {
                int size = bitmaps.Count;
                allowModify = size == 0;
                for (int i = 0; i < size; ++i)
                {
                    Thread.Sleep(1000 / frameTime);
                    if (needModify)
                    {
                        allowModify = true;
                        while (needModify) ;
                        break;
                    }
                    manager.SetBits(bitmaps[i]);
                }
            }
            while (!manager.IsDisposed) ;
        }

        public void RequestDispose()
        {
            closed = true;
            window.Invoke((MethodInvoker)(() => window.Dispose()));
            while (!window.IsDisposed) ;
        }
    }
}
