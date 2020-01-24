using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkDesktop.CoreKit
{
    public static class PackageManager
    {
        public class PackageInfo
        {
            public byte version;
        }
        public class PackagedConfigInfo
        {
            public Guid configGuid;
            public string defaultConfigName;
            public int fileCount;
            public long firstFilePosition;
        }

        public static string ToNiceRelativePath(string path) => path[0] == '/' || path[0] == '\\' ? path.Substring(1) : path;

        public static void EnsureDirectoryExists(string path)
        {
            path = Path.GetDirectoryName(path);
            if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
        }

        public static void ExtractFiles(string rootPath, Stream stream)
        {
            int fileCount = ReadInt32(stream);
            while (fileCount-- != 0)
            {
                string fileName = ReadString(stream);
                int fileLength = ReadInt32(stream);
                var path = Path.Combine(rootPath, ToNiceRelativePath(fileName));
                EnsureDirectoryExists(path);
                using (var fs = File.OpenWrite(path))
                {
                    while (fileLength != 0)
                    {
                        var buf = new byte[8196];
                        int readLength = fileLength > 8196 ? 8196 : fileLength;
                        stream.Read(buf, 0, readLength);
                        fs.Write(buf, 0, readLength);
                        fileLength -= readLength;
                    }
                }
            }
        }

        public static (PackageInfo, List<PackagedConfigInfo>) ReadPackageInfo(Stream stream)
        {
            byte[] magic = ReadBytes(stream, 4);
            if ("AKDP" != Encoding.ASCII.GetString(magic)) return (new PackageInfo { version = 255 }, null);
            if (stream.ReadByte() != 2) return (new PackageInfo { version = 254 }, null);
            int configCount = ReadInt32(stream);
            var retList = new List<PackagedConfigInfo>();
            while (configCount-- != 0)
            {
                var single = new PackagedConfigInfo();
                single.configGuid = new Guid(ReadBytes(stream, 16));
                single.defaultConfigName = ReadString(stream);
                single.firstFilePosition = stream.Position;
                single.fileCount = ReadInt32(stream);
                retList.Add(single);
                for (int i = 0; i != single.fileCount; ++i)
                {
                    _ = ReadString(stream);
                    int fileLength = ReadInt32(stream);
                    stream.Position += fileLength;
                }
            }
            return (new PackageInfo { version = 2 }, retList);
        }

        public static void PackConfigs(List<ConfigInfo> configsInfos, string path)
        {
            using (var fs = File.OpenWrite(path))
            {
                WriteBytes(fs, Encoding.ASCII.GetBytes("AKDP"));//固定
                fs.WriteByte(2);//guding
                WriteInt32(fs, configsInfos.Count);//数量
                foreach (var configInfo in configsInfos)
                {
                    WriteBytes(fs, configInfo.ConfigGuid.ToByteArray());
                    WriteString(fs, configInfo.ConfigName);
                    var vs = EnumDirectory(configInfo.rootPath);
                    WriteInt32(fs, vs.Count);
                    foreach (var p in vs)
                    {
                        WriteString(fs, p.Substring(configInfo.rootPath.Length));
                        WriteInt32(fs, GetFileSize(p));
                        using (FileStream fs1 = File.OpenRead(p)) fs1.CopyTo(fs);
                    }
                }
            }
        }

        private static int GetFileSize(string path)//获取文件长度
        {
            FileInfo fileInfo = new FileInfo(path);
            return Convert.ToInt32(fileInfo.Length);
        }

        private static List<string> EnumDirectory(string dir)
        {
            var vs = new List<string>();
            DirectoryInfo d = new DirectoryInfo(dir);
            FileSystemInfo[] fileSystemInfos = d.GetFileSystemInfos();
            foreach (var str in fileSystemInfos)
            {
                if (str is DirectoryInfo) vs.AddRange(EnumDirectory(str.FullName));
                else vs.Add(str.FullName);
            }
            return vs;
        }
        public static int ReadInt32(Stream stream)
        {
            var arry = ReadBytes(stream, 4);
            int ret = 0;
            for (int i = 0; i != 4; ++i)
                ret |= (arry[i]) << (8 * i);
            return ret;
        }
        static bool ConvertIntToByteArray(Int32 m, ref byte[] arry)
        {
            if (arry == null) return false;
            if (arry.Length < 4) return false;

            arry[0] = (byte)(m & 0xFF);
            arry[1] = (byte)((m & 0xFF00) >> 8);
            arry[2] = (byte)((m & 0xFF0000) >> 16);
            arry[3] = (byte)((m >> 24) & 0xFF);

            return true;
        }
        public static void WriteInt32(Stream stream, int s)
        {
            byte[] cov = new byte[4];
            ConvertIntToByteArray(s, ref cov);
            stream.Write(cov, 0, 4);
        }
        public static byte[] ReadBytes(Stream stream, int count)
        {
            byte[] ret = new byte[count];
            stream.Read(ret, 0, count);
            return ret;
        }
        public static void WriteBytes(Stream stream, byte[] s)
        {
            stream.Write(s, 0, s.Length);
        }
        public static string ReadString(Stream stream)
        {
            var vs = new List<byte>();
            while (true)
            {
                int read = stream.ReadByte();
                if (read > 0) vs.Add(unchecked((byte)read));
                else break;
            }
            return Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.ASCII.GetString(vs.ToArray())));
        }
        public static void WriteString(Stream stream, string s)
        {
            WriteBytes(stream, Encoding.ASCII.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(s))));
            stream.WriteByte(0);
        }
    }
}
