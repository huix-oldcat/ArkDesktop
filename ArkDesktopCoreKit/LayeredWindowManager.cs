using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using static ArkDesktop.CoreKit.LayeredWindowManager;

namespace ArkDesktop.CoreKit
{
    public partial class LayeredWindowManager : UserControl
    {
        #region General variables

        /// <summary>
        /// Managed layered window
        /// </summary>
        public LayeredWindow window;
        /// <summary>
        /// Manager's xml config node -- provided by host
        /// </summary>
        public XElement config;
        /// <summary>
        /// Thread used to render managed layered window
        /// </summary>
        public Thread renderThread;
        /// <summary>
        /// If the manager is ready.
        /// Including launching the managed window.
        /// </summary>
        public ManualResetEvent Ready { get; private set; } = new ManualResetEvent(false);
        private ResourceManager resourceManager;

        #endregion

        #region Managed window features
        public class WindowFeatures
        {
            public enum ManageLevel
            {
                Pass, Direct, Hideden
            }
            public ManageLevel topMost = ManageLevel.Pass;
            public ManageLevel zoom = ManageLevel.Pass;
            public ManageLevel zoomQuality = ManageLevel.Pass;
            public ManageLevel windowLocation = ManageLevel.Pass;
        }
        private readonly WindowFeatures settings;

        public bool TopMost
        {
            get => checkBox_TopMost.Checked;//TODO
            set
            {
                if (TopMost && settings.topMost == WindowFeatures.ManageLevel.Pass)
                {
                    window.Invoke((MethodInvoker)(() =>
                    {
                        window.timer.Interval = 50;
                        window.timer.Tick -= TopMost_Tick;
                        window.timer.Tick += TopMost_Tick;
                        window.timer.Enabled = true;
                    }));
                }
                if (TopMost)
                {
                    if (config.Element("TopMost") == null)
                        config.Add(new XElement("TopMost"));
                }
                else
                {
                    if (config.Element("TopMost") != null)
                        config.Element("TopMost").Remove();
                }
            }
        }

        private void TopMost_Tick(object sender, EventArgs e)
        {
            Win32.SetWindowPos(window.Handle, new IntPtr(-1), 0, 0, 0, 0, 2 | 1);
            if (TopMost == false)
            {
                window.timer.Enabled = false;
            }
        }

        public enum ZoomQualityLevel
        {
            HighQuality,
            HighSpeed
        }

        private double zoomLevel;
        ZoomQualityLevel zoomQuality = ZoomQualityLevel.HighQuality;

        public double ZoomLevel
        {
            get => zoomLevel;
            set
            {
                zoomLevel = value;
                linkLabel_Zoom.Text = value.ToString() + 'x';
                config.Element("Zoom").Value = value.ToString();
            }
        }

        public ZoomQualityLevel ZoomQuality
        {
            get => zoomQuality;
            set
            {
                zoomQuality = value;
                linkLabel_ZoomQuality.Text = value == ZoomQualityLevel.HighQuality ? "高质量" : "高速度";
                config.Element("ZoomQuality").Value = value == ZoomQualityLevel.HighQuality ? "Quality" : "Speed";
            }
        }

        public Bitmap ZoomBits(Bitmap bitmap)
        {
            Bitmap zoomed = new Bitmap((int)(bitmap.Width * zoomLevel), (int)(bitmap.Height * zoomLevel));
            using (Graphics g = Graphics.FromImage(zoomed))
            {
                ApplyZoomQuality(g);
                Rectangle rect = new Rectangle(0, 0, (int)(bitmap.Width * zoomLevel), (int)(bitmap.Height * zoomLevel));
                g.DrawImage(bitmap, rect);
            }
            return zoomed;
        }

        public bool HideTaskbarIcon
        {
            set
            {
                window.Invoke((MethodInvoker)(() => window.ShowInTaskbar = !value));
            }
            get => checkBox_HideTaskbarIcon.Checked;
        }

        public bool TransparentEvents
        {
            set
            {
                window.Invoke((MethodInvoker)(() =>
                {
                    int origin = Win32.GetWindowLong(window.Handle, Win32.GWL_EXSTYLE);
                    bool isSet = (origin & Win32.WS_EX_TRANSPARENT) != 0;
                    if (isSet != value)
                    {
                        if (value) origin |= Win32.WS_EX_TRANSPARENT;
                        else origin ^= Win32.WS_EX_TRANSPARENT;
                    }
                    Win32.SetWindowLong(window.Handle, Win32.GWL_EXSTYLE, unchecked((uint)origin));
                    config.Element("TransparentEvents").Value = value ? "True" : "False";
                }));
            }
            get => TransparentEventsCheckBox.Checked;
        }

        private byte _imageTransparency;
        public byte ImageTransparency
        {
            get => _imageTransparency;
            set
            {
                _imageTransparency = value;
                TransparentImageLinkLabel.Text = string.Format("透明度：{0}/255", value);
                config.Element("ImageTransparancy").Value = value.ToString();
            }
        }

        //SetBits --> using (ZoomBitmap) { Window.SetBits() }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            config.Element("Location").Element("X").Value = ((LayeredWindow)sender).Location.X.ToString();
            config.Element("Location").Element("Y").Value = ((LayeredWindow)sender).Location.Y.ToString();
        }
        #endregion

        #region Window Location Manage

        private bool isMouseDown = false;
        private Point mousePoint;
        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            mousePoint = e.Location;
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void Button_Pos_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == false) return;
            Point point = window.Location;
            point.X += (e.X - mousePoint.X);
            point.Y += (e.Y - mousePoint.Y);
            window.Invoke((MethodInvoker)(() => window.Location = point));
            mousePoint = e.Location;
        }

        private void SetLocationDirect(Point point)
        {
            window.Invoke((MethodInvoker)(() => window.Location = point));
        }

        #endregion

        /// <summary>
        /// Move window under the zoom level.
        /// You shoule not use this method if you set zoom != Pass
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public void MoveWindow(int deltaX, int deltaY)
        {
            if (window.InvokeRequired)
            {
                window.Invoke((MethodInvoker)(() => MoveWindow(deltaX, deltaY)));
                return;
            }
            window.Top += Convert.ToInt32(deltaY * zoomLevel);
            window.Left += Convert.ToInt32(deltaX * zoomLevel);
        }

        private void LaunchManagedWindow()
        {
            renderThread = new Thread(new ThreadStart(() => { Application.Run(window); }));
            renderThread.IsBackground = true;
            renderThread.Start();
            window.Ready.WaitOne();
        }

        public LayeredWindowManager(XElement config, WindowFeatures settings = null, LayeredWindow window = null)
        {
            if (settings == null) settings = new WindowFeatures();
            if (window != null) this.window = window; else this.window = new LayeredWindow();
            if (config == null) config = new XElement("Config");
            this.config = config;
            this.settings = settings;
            InitializeComponent();
        }
        public LayeredWindowManager(ResourceManager resource, WindowFeatures settings = null, LayeredWindow window = null)
        {
            resourceManager = resource;
            if (settings == null) settings = new WindowFeatures();
            if (window != null) this.window = window; else this.window = new LayeredWindow();
            this.config = resource.GetConfig("ArkDesktop.LayeredWindowManager");
            this.settings = settings;
            InitializeComponent();
        }

        public new void Dispose()
        {
            resourceManager?.SaveConfig("ArkDesktop.LayeredWindowManager");
            base.Dispose(true);
            window.Invoke((MethodInvoker)(() => window.Dispose()));
        }

        private bool ready = false;
        private void LayeredWindowManager_Load(object sender, EventArgs e)
        {
            if (ready) return;
            LaunchManagedWindow();
            CheckConfigNode();
            SetLocationDirect(new Point(Convert.ToInt32(config.Element("Location").Element("X").Value),
                                        Convert.ToInt32(config.Element("Location").Element("Y").Value)));
            ZoomLevel = Convert.ToDouble(config.Element("Zoom").Value);
            ZoomQuality = config.Element("ZoomQuality").Value == "Speed" ? ZoomQualityLevel.HighSpeed : ZoomQualityLevel.HighQuality;
            checkBox_HideTaskbarIcon.Checked = config.Element("HideTaskbarIcon").Value == "True";
            checkBox_TopMost.Checked = config.Element("TopMost") != null;
            TransparentEventsCheckBox.Checked = config.Element("TransparentEvents").Value == "True";
            int read_tr = Convert.ToInt32(config.Element("ImageTransparancy").Value);
            if (read_tr > 255 || read_tr < 0) read_tr = 255;
            ImageTransparency = unchecked((byte)read_tr);
            if (settings.windowLocation != WindowFeatures.ManageLevel.Hideden) this.window.LocationChanged += Window_LocationChanged;
            Ready.Set();
            ready = true;
        }

        private void CheckConfigNode()
        {
            if (config == null)
                config = new XElement("Config");

            //Location
            if (config.Element("Location") == null)
                config.Add(new XElement("Location", new XElement("X", 1), new XElement("Y", 1)));
            else
            {
                if (config.Element("Location").Element("X") == null)
                    config.Element("Location").Add("X", 1);
                if (config.Element("Location").Element("Y") == null)
                    config.Element("Location").Add("Y", 1);
            }

            //Zoom
            if (config.Element("Zoom") == null)
                config.Add(new XElement("Zoom", 1));
            if (config.Element("ZoomQuality") == null)
                config.Add(new XElement("ZoomQuality", ZoomQualityLevel.HighQuality.ToString()));

            //HideTaskbarIcon
            if (config.Element("HideTaskbarIcon") == null)
                config.Add(new XElement("HideTaskbarIcon", "False"));

            //Zorder : null

            //TransparentEvents
            if (config.Element("TransparentEvents") == null)
                config.Add(new XElement("TransparentEvents", "False"));

            //TransparentImage
            if (config.Element("ImageTransparancy") == null)
                config.Add(new XElement("ImageTransparancy", "255"));
        }

        //public Bitmap ZoomBits(Bitmap bitmap, double level)
        //{
        //    Bitmap zoomed = new Bitmap((int)(bitmap.Width * level), (int)(bitmap.Height * level));
        //    using (Graphics g = Graphics.FromImage(zoomed))
        //    {
        //        ApplyZoomQuality(g);
        //        Rectangle rect = new Rectangle(0, 0, (int)(bitmap.Width * level), (int)(bitmap.Height * level));
        //        g.DrawImage(bitmap, rect);
        //    }
        //    return zoomed;
        //}

        public void SetBits(Bitmap bitmap)
        {
            bool needDispose = false;
            Bitmap realBitmap = bitmap;
            if (settings.zoom == WindowFeatures.ManageLevel.Pass && zoomLevel != 1)
            {
                realBitmap = ZoomBits(bitmap);
                needDispose = true;
            }
            if (realBitmap.Size != window.Size)
            {
                window.Invoke((MethodInvoker)(() => window.Size = realBitmap.Size));
            }
            window.Invoke((MethodInvoker)(() => window.SetBits(realBitmap, ImageTransparency)));
            if (needDispose) realBitmap.Dispose();
        }

        private void LinkLabel_Zoom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string input = Interaction.InputBox("输入新的缩放值（小数）", "QwQ", zoomLevel.ToString());
            if (input != "")
            {
                var inp = Convert.ToDouble(input);
                if (inp <= 0)
                    inp = 1;
                ZoomLevel = inp;
            }
        }

        private void LinkLabel_ZoomQuality_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            switch (zoomQuality)
            {
                case ZoomQualityLevel.HighQuality:
                    zoomQuality = ZoomQualityLevel.HighSpeed;
                    break;
                case ZoomQualityLevel.HighSpeed:
                    zoomQuality = ZoomQualityLevel.HighQuality;
                    break;
            }
            switch (zoomQuality)
            {
                case ZoomQualityLevel.HighQuality:
                    linkLabel_ZoomQuality.Text = "高质量";
                    config.Element("ZoomQuality").Value = "Quality";
                    break;
                case ZoomQualityLevel.HighSpeed:
                    linkLabel_ZoomQuality.Text = "高速度";
                    config.Element("ZoomQuality").Value = "Speed";
                    break;
            }
        }

        private void ApplyZoomQuality(Graphics g)
        {
            switch (zoomQuality)
            {
                case ZoomQualityLevel.HighQuality:
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    break;
                case ZoomQualityLevel.HighSpeed:
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                    break;
            }
        }

        private void Window_SizeChanged(object sender, EventArgs e)
        {
            if ((sender as LayeredWindow).WindowState == FormWindowState.Minimized)
            {
                (sender as LayeredWindow).WindowState = FormWindowState.Normal;
            }
        }
        private void CheckBox_ShowTaskbarIcon_CheckedChanged(object sender, EventArgs e)
        {
            HideTaskbarIcon = checkBox_HideTaskbarIcon.Checked;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("拖动这个按钮,内容即会同步移动", "QwQ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkBox_TopMost_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = true;
        }

        private void TransparentEventsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TransparentEvents = TransparentEventsCheckBox.Checked;
        }

        private void TransparentImageLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string input = Interaction.InputBox("输入新的透明度(0透明~255不透明)", "QwQ", ImageTransparency.ToString());
            try
            {
                int to = Convert.ToInt32(input);
                if (to < 0) to = 0;
                if (to > 255) to = 255;
                ImageTransparency = unchecked((byte)to);
            }
            catch(Exception)
            { }
        }
    }
}
