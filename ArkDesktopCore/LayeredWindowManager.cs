using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ArkDesktop
{
    public partial class LayeredWindowManager : UserControl
    {
        public LayeredWindow window;
        public bool CanModifyBitmap = false;
        public event MethodInvoker OnWindowPositionChange;
        public Config config;
        public bool helpZoomChange;
        public bool TopMost
        {
            get
            {
                return topMost;
            }
            set
            {
                topMost = value;
                if (topMost)
                {
                    window.Invoke((MethodInvoker)(() =>
                    {
                        window.timer.Interval = 50;
                        window.timer.Tick -= TopMost_Tick;
                        window.timer.Tick += TopMost_Tick;
                        window.timer.Enabled = true;
                    }));
                }
            }
        }

        private void TopMost_Tick(object sender, EventArgs e)
        {
            Win32.SetWindowPos(window.Handle, new IntPtr(-1), 0, 0, 0, 0, 2 | 1);
            if (topMost == false)
            {
                window.timer.Enabled = false;
            }
        }

        public double Zoom
        {
            get => zoom;
            set
            {
                zoom = value;
                config.GetElement(ns + "LayeredWindowManager").Element(ns + "Zoom").Value = zoom.ToString();
            }
        }
        public enum ZoomQuality
        {
            HighQuality,
            HighSpeed
        }
        ZoomQuality zoomQuality = ZoomQuality.HighQuality;

        private bool isMouseDown = false;
        private Point mousePoint;
        private Thread renderThread;
        private XNamespace ns = "ArkDesktop";
        private double zoom = 1.0;
        private bool topMost;

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
                if (config.GetElement(ns + "LayeredWindowManager").Element(ns + "Zoom") == null)
                {
                    config.GetElement(ns + "LayeredWindowManager").Add(new XElement(ns + "Zoom", "1"));
                }
                if (config.GetElement(ns + "LayeredWindowManager").Element(ns + "ZoomQuality") == null)
                {
                    config.GetElement(ns + "LayeredWindowManager").Add(new XElement(ns + "ZoomQuality", "Quality"));
                }

                zoom = Convert.ToDouble(config.GetElement(ns + "LayeredWindowManager").Element(ns + "Zoom").Value);
                linkLabel_Zoom.Text = "x" + zoom.ToString();
                if (config.GetElement(ns + "LayeredWindowManager").Element(ns + "ZoomQuality").Value == "Speed")
                {
                    linkLabel_ZoomQuality.Text = "高速度";
                    zoomQuality = ZoomQuality.HighSpeed;
                }
                else
                {
                    linkLabel_ZoomQuality.Text = "高质量";
                    zoomQuality = ZoomQuality.HighQuality;
                }
            }
            Ready = true;
        }

        public Bitmap ZoomBits(Bitmap bitmap, double level)
        {
            Bitmap zoomed = new Bitmap((int)(bitmap.Width * level), (int)(bitmap.Height * level));
            using (Graphics g = Graphics.FromImage(zoomed))
            {
                ApplyZoomQuality(g);
                Rectangle rect = new Rectangle(0, 0, (int)(bitmap.Width * level), (int)(bitmap.Height * level));
                g.DrawImage(bitmap, rect);
            }
            return zoomed;
        }

        public void SetBits(Bitmap bitmap)
        {
            bool needDispose = false;
            Bitmap realBitmap = bitmap;
            if (helpZoomChange && Zoom != 1)
            {
                realBitmap = ZoomBits(bitmap, Zoom);
                needDispose = true;
            }
            if (realBitmap.Size != window.Size)
            {
                window.Invoke((MethodInvoker)(() => window.Size = realBitmap.Size));
            }
            window.Invoke((MethodInvoker)(() => window.SetBits(realBitmap)));
            if (needDispose)
            {
                realBitmap.Dispose();
            }
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
                        new XElement(ns + "X", "1"),
                        new XElement(ns + "Y", "1")
                    )
                )
            );
        }

        private void LinkLabel_Zoom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string input = Interaction.InputBox("输入新的缩放值（小数）", "QwQ", Zoom.ToString());
            if (input != "")
            {
                Zoom = Convert.ToDouble(input);
                if (Zoom <= 0)
                    Zoom = 1;
                linkLabel_Zoom.Text = "x" + Zoom.ToString();
            }
        }

        private void LinkLabel_ZoomQuality_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            switch (zoomQuality)
            {
                case ZoomQuality.HighQuality:
                    zoomQuality = ZoomQuality.HighSpeed;
                    break;
                case ZoomQuality.HighSpeed:
                    zoomQuality = ZoomQuality.HighQuality;
                    break;
            }
            switch (zoomQuality)
            {
                case ZoomQuality.HighQuality:
                    linkLabel_ZoomQuality.Text = "高质量";
                    config.GetElement(ns + "LayeredWindowManager").Element(ns + "ZoomQuality").Value = "Quality";
                    break;
                case ZoomQuality.HighSpeed:
                    linkLabel_ZoomQuality.Text = "高速度";
                    config.GetElement(ns + "LayeredWindowManager").Element(ns + "ZoomQuality").Value = "Speed";
                    break;
            }
        }

        private void ApplyZoomQuality(Graphics g)
        {
            switch (zoomQuality)
            {
                case ZoomQuality.HighQuality:
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    break;
                case ZoomQuality.HighSpeed:
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                    break;
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "默认"://TODO
                    TopMost = false;
                    window.Invoke((MethodInvoker)(() => { window.TopMost = false; window.TopLevel = false; }));
                    window.Invoke((MethodInvoker)(() => Win32.SetWindowLong(window.Handle, -8, 0)));
                    break;
                case "置于桌面":
                    TopMost = false;
                    window.Invoke((MethodInvoker)(() => { window.TopMost = false; window.TopLevel = false; }));
                    IntPtr intPtr = WindowPositionHelper.GetDekstopLayerHwnd();
                    if (intPtr != IntPtr.Zero)
                    {
                        window.Invoke((MethodInvoker)(() => Win32.SetWindowLong(window.Handle, -8, (uint)intPtr.ToInt32())));
                    }
                    break;
                case "置顶（强力）":
                    TopMost = true;
                    window.Invoke((MethodInvoker)(() => Win32.SetWindowLong(window.Handle, -8, 0)));
                    break;
            }
        }
    }
}
