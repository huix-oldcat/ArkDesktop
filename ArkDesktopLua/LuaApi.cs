using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Neo.IronLua;
using System.Reflection;
using System.Threading;
using System.Media;

namespace ArkDesktopLua
{
    public class LuaApi
    {
        public List<Bitmap> bitmaps = new List<Bitmap>();
        public ArkDesktopLuaModule master;
        private readonly Lua lua;
        public dynamic env;
        public string clickMethod = "";
        public Bitmap draft;
        public bool autoClearBackground = true;
        public bool reverse = false;
        public bool strictMode = false;
        public int reversePos = 0;
        public LuaChunk clickMethodChunk;
        public SoundPlayer player;

        public int LoadBitmap(string relativePath, bool necessary = true)
        {
            Stream stream = master.resourceManager.OpenRead(relativePath);
            if (stream.Length == 0)
            {
                if (necessary)
                    throw new Exception("Bitmap \"" + relativePath + "\" not found.");
                else return -1;
            }
            bitmaps.Add(new Bitmap(stream));
            return bitmaps.Count - 1;
        }

        public bool DisplayBitmap(int index)
        {
            if (index < 0 || index >= bitmaps.Count)
                if (strictMode)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
                else
                    return false;
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
            if (methodName == "") clickMethodChunk = null;
            else clickMethodChunk = lua.CompileChunk(methodName + "()", "script.lua", new LuaCompileOptions());
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
            if (index < 0 || index >= bitmaps.Count)
                if (strictMode)
                    throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");
                else
                    return false;
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
                case "strictMode":
                    {
                        strictMode = flagValue;
                        return;
                    }
            }
        }

        // add playing sound function
        public void PlaySound(string relativePath)
        {
            string realPath = master.resourceManager.GetResRealPath(relativePath);
            if (File.Exists(realPath))
            {
                player = new SoundPlayer(realPath);
                player.Play();
            }
            else throw new ArgumentException($"{nameof(relativePath)}: doesn't exist.");


        }

        public void OnClick()
        {
            if (clickMethodChunk == null) return;
            try
            {
                env.dochunk(clickMethodChunk);
            }
            catch (Exception) { };
        }

        public void RegisterLua()
        {
            env.LoadBitmap = new Func<string, bool, int>(LoadBitmap);
            env.DisplayBitmap = new Func<int, bool>(DisplayBitmap);
            env.Sleep = new Action<int>(Sleep);
            env.RequestClickEvent = new Action<string>(RequestClickEvent);
            env.CreateDraft = new Action<int, int>(CreateDraft);
            env.CopyBitmapToDraft = new Func<int, int, int, bool>(CopyBitmapToDraft);
            env.DrawDraft = new Action(DrawDraft);
            env.MoveWindow = new Action<int, int>(MoveWindow);
            env.SetFlag = new Action<string, bool>(SetFlag);
            env.PlaySound = new Action<string>(PlaySound);
        }

        public LuaApi(ArkDesktopLuaModule master, Lua lua, dynamic env)
        {
            this.master = master ?? throw new ArgumentNullException(nameof(master));
            this.lua = lua;
            this.env = env ?? throw new ArgumentNullException(nameof(env));
            RegisterLua();
        }
    }
}
