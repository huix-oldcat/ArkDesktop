using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Xml.Linq;

namespace ArkDesktop
{
    public partial class LayeredWindowManager : UserControl
    {
        public LayeredWindow window;
        public bool CanModifyBitmap = false;
        public event MethodInvoker OnWindowPositionChange;
        public Config config;

        private bool isMouseDown = false;
        private Point mousePoint;
        private Thread renderThread;
        private XNamespace ns = "ArkDesktop";

        public bool Ready { get; private set; } = false;

        public LayeredWindowManager()
        {
            InitializeComponent();
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            mousePoint = e.Location;
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            OnWindowPositionChange();
        }

        private void Button_Pos_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == false)
            {
                return;
            }
            Point point = window.Location;
            point.X += (e.X - mousePoint.X);
            point.Y += (e.Y - mousePoint.Y);
            window.Invoke((MethodInvoker)(() => window.Location = point));
            mousePoint = e.Location;
        }

        private void LayeredWindowManager_Load(object sender, EventArgs e)
        {
            if (Ready)
            {
                return;
            }
            renderThread = new Thread(new ThreadStart(() => { Application.Run(window); }));
            renderThread.IsBackground = true;
            renderThread.Start();
            while (!window.Ready) ;
            if (config != null && config.GetElement(ns + "LayeredWindowManager") != null)
            {
                window.Invoke((MethodInvoker)(() => window.Location = new Point(
                    Convert.ToInt32(config.GetElement(ns + "LayeredWindowManager").Element(ns + "Position").Element(ns + "X").Value),
                    Convert.ToInt32(config.GetElement(ns + "LayeredWindowManager").Element(ns + "Position").Element(ns + "Y").Value))));
            }
            Ready = true;
        }

        public void SetBits(Bitmap bitmap)
        {
            if (bitmap.Size != window.Size)
            {
                window.Invoke((MethodInvoker)(() => window.Size = bitmap.Size));
            }
            window.Invoke((MethodInvoker)(() => window.SetBits(bitmap)));
        }

        private void ManagedWindowPositionChange()
        {
            config.GetElement(ns + "LayeredWindowManager").Element(ns + "Position").Element(ns + "X").Value = window.Location.X.ToString();
            config.GetElement(ns + "LayeredWindowManager").Element(ns + "Position").Element(ns + "Y").Value = window.Location.Y.ToString();
        }

        public void HelpPositionChange()
        {
            if (config == null)
            {
                throw new Exception("Config is null");
            }
            if (config.GetElement(ns + "LayeredWindowManager") == null)
            {
                CreateConfig();
            }
            OnWindowPositionChange += ManagedWindowPositionChange;
        }

        private void CreateConfig()
        {
            config.AppendElement(
                new XElement(ns + "LayeredWindowManager",
                    new XElement(ns + "Position",
                        new XElement("X", "1"),
                        new XElement("Y", "1")
                    )
                )
            );
        }
    }
}
