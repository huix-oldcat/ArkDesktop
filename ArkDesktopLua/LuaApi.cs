using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LuaInterface;
using System.Reflection;
using System.Threading;

namespace ArkDesktopLua
{
    public class LuaApi
    {
        public List<Bitmap> bitmaps = new List<Bitmap>();
        public ArkDesktopLua master;
        public Lua lua;
        public string clickMethod = "";

        public int LoadBitmap(string relativePath, bool necessary = true)
        {
            string realPath = Path.GetFullPath(Path.Combine(master.core.RootPath, "./Resources/ArkDesktop.StaticPic/", "./"+relativePath)), rootPath = Path.GetFullPath(Path.Combine(master.core.RootPath, "./Resources/"));
            realPath = Path.GetFullPath(realPath);
            FileInfo info = new FileInfo(realPath);
            if (info.Exists == false) if (necessary) throw new Exception("Bitmap \"" + realPath + "\" not found."); else return -1;
            if (info.FullName.Substring(0, rootPath.Length) != rootPath)if (necessary) throw new Exception("Bitmap \"" + realPath + "\" is not int the resource dir."); else return -1;
            bitmaps.Add(new Bitmap(realPath));
            return bitmaps.Count - 1;
        }

        public bool DisplayBitmap(int index)
        {
            if (index < 0 || index >= bitmaps.Count) return false;
            master.manager.SetBits(bitmaps[index]);
            return true;
        }

        public void Sleep(int millisecondsTimeout)
        {
            Thread.Sleep(millisecondsTimeout);
        }

        public void RequestClickEvent(string methodName)
        {
            clickMethod = methodName;
        }

        public void OnClick()
        {
            if (clickMethod == "") return;
            try
            {
                lua.DoString(clickMethod + "()");
            }
            catch (Exception) { };
        }

        public void RegisterLua()
        {
            lua.RegisterFunction("LoadBitmap", this, typeof(LuaApi).GetMethod("LoadBitmap"));
            lua.RegisterFunction("DisplayBitmap", this, typeof(LuaApi).GetMethod("DisplayBitmap"));
            lua.RegisterFunction("Sleep", this, typeof(LuaApi).GetMethod("Sleep"));
            lua.RegisterFunction("RequestClickEvent", this, typeof(LuaApi).GetMethod("RequestClickEvent"));
        }

        public LuaApi(ArkDesktopLua master, Lua lua)
        {
            this.master = master ?? throw new ArgumentNullException(nameof(master));
            this.lua = lua ?? throw new ArgumentNullException(nameof(lua));
            RegisterLua();
        }
    }
}
