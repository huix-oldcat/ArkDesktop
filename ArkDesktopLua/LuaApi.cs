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
        public Bitmap draft;
        public bool autoClearBackground = true;
        public bool reverse = false;
        public int reversePos = 0;

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

        public void CreateDraft(int width, int height)
        {
            if (draft != null)
            {
                draft.Dispose();
            }
            draft = new Bitmap(width, height);
        }

        public bool CopyBitmapToDraft(int index, int dx = 0, int dy = 0)
        {
            if (index < 0 || index >= bitmaps.Count) return false;
            using (Graphics g = Graphics.FromImage(draft))
            {
                if (autoClearBackground)
                {
                    g.Clear(Color.Transparent);
                }
                if (reverse)
                {
                    dx = draft.Width - dx - bitmaps[index].Width;
                    using (Image i = new Bitmap(bitmaps[index]))
                    {
                        i.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        g.DrawImage(i, dx, dy);
                    }
                }
                else
                {
                    g.DrawImage(bitmaps[index], dx, dy);
                }
            }
            return true;
        }

        public void DrawDraft()
        {
            master.manager.SetBits(draft);
        }

        public void MoveWindow(int deltaX, int deltaY)
        {
            master.manager.MoveWindow((reverse ? -1 : 1) * deltaX, deltaY);
        }

        public void SetFlag(string flagName, bool flagValue)
        {
            switch (flagName)
            {
                case "autoClearBackground":
                    {
                        autoClearBackground = flagValue;
                        return;
                    }
                case "reverse":
                    {
                        reverse = flagValue;
                        return;
                    }
            }
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
            lua.RegisterFunction("CreateDraft", this, typeof(LuaApi).GetMethod("CreateDraft"));
            lua.RegisterFunction("CopyBitmapToDraft", this, typeof(LuaApi).GetMethod("CopyBitmapToDraft"));
            lua.RegisterFunction("DrawDraft", this, typeof(LuaApi).GetMethod("DrawDraft"));
            lua.RegisterFunction("MoveWindow", this, typeof(LuaApi).GetMethod("MoveWindow"));
            lua.RegisterFunction("SetFlag", this, typeof(LuaApi).GetMethod("SetFlag"));
        }

        public LuaApi(ArkDesktopLua master, Lua lua)
        {
            this.master = master ?? throw new ArgumentNullException(nameof(master));
            this.lua = lua ?? throw new ArgumentNullException(nameof(lua));
            RegisterLua();
        }
    }
}
