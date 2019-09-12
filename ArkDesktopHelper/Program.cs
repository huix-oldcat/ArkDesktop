using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArkDesktop;

namespace ArkDesktopHelper
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Core coreInst = null;
            Thread coreThread = new Thread(new ThreadStart(() => coreInst = new Core(AppDomain.CurrentDomain.BaseDirectory)));
            coreThread.Start();
            while (coreInst == null) ;
            if (Environment.GetCommandLineArgs().Length != 2 || Environment.GetCommandLineArgs()[1] != "-autorun")
            {
                Application.Run(new Form1(coreInst));
                if (coreInst.config.GetLaunchPlugin() == null)
                {
                    return;
                }
            }
            else
            {
                bool success = false;
                foreach (Config.ConfigInfo info in coreInst.config.ConfigList)
                {
                    if (info.isDefault)
                    {
                        success = true;
                        coreInst.config.ChangeActiveConfig(info.configName);
                        break;
                    }
                }
                if (success == false)
                {
                    return;
                }
            }
            coreInst.CreateInst(coreInst.config.GetLaunchPlugin());
            coreInst.MainThread();
        }
    }
}
