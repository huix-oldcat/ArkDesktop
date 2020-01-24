using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace ArkDesktop.CoreKit
{
    public static class UpdateChecker
    {
        public static (string, string) GetUpdateInfo()
        {
            WebClient client = new WebClient();
            try
            {
                var st = client.OpenRead("https://akd.huix.cc/api/GetUpdate.php?current=" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
                string s;
                using (StreamReader sr = new StreamReader(st)) s = sr.ReadToEnd();
                if (s == "Latest") return ("Latest", null);
                var a = s.Split('|');
                if (a.Length < 2) return ("", null);
                if (a.Length > 2)
                {
                    string a1 = "";
                    for (int i = 1; i != a.Length; ++i) a1 += a[i] + '|';
                    a1 = a1.Substring(0, a1.Length - 1);
                }
                return (a[0], a[1]);
            }
            catch (Exception)
            {
                return ("", null);
            }
        }
        public static string CoreVersion { get => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); }

    }
}
