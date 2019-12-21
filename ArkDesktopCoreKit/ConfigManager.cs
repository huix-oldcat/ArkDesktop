using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ArkDesktop.CoreKit
{
    public static class BinaryFileHelper
    {
        public static int ReadInt32(Stream stream)
        {
            byte[] cov = new byte[4];
            for (int i = 0; i < 4; ++i) cov[i] = (byte)stream.ReadByte();
            return BitConverter.ToInt32(cov, 0);
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
        public static void WriteString(Stream stream, string s)
        {
            WriteBytes(stream, Encoding.UTF8.GetBytes(s));
        }
    }

    public class ResourceManager
    {
        private string rootPath;
        private FileStream packageStream;
        private Dictionary<string, Tuple<int, int>> packagedFile = new Dictionary<string, Tuple<int, int>>();
        private Dictionary<string, XDocument> activeConfigs = new Dictionary<string, XDocument>();
        public ResourceManager(string rootPath)
        {
            this.rootPath = rootPath;
            if (Directory.Exists(rootPath) == false)
            {
                throw new ArgumentException("Cannot found specified path.", nameof(rootPath));
            }
            if (File.Exists(Path.Combine(rootPath, "resources.akdres")))
            {
                packageStream = File.OpenRead(Path.Combine(rootPath, "resources.akdres"));
                ReadFileList();
            }
        }

        private bool CheckInDir(string realPath)
        {
            return true;
        }

        public Stream OpenRead(string filename, bool packagedFirst = false)
        {
            bool packaged = false;
            if (packagedFile.ContainsKey(filename))
            {
                packaged = true;
                if (packagedFirst)
                    throw new NotImplementedException("Lazy...");
            }
            string realPath = Path.Combine(rootPath, filename);
            if (CheckInDir(realPath) == false)
            {
                return new MemoryStream();
            }
            if (File.Exists(realPath))
            {
                return File.OpenRead(realPath);
            }
            else if (packaged)
            {
                throw new NotImplementedException("Lazy...");
            }
            return new MemoryStream();
        }

        public Stream OpenWrite(string filename)
        {
            string realPath = Path.Combine(rootPath, filename);
            if (CheckInDir(realPath) == false)
            {
                return new MemoryStream();
            }
            throw new NotImplementedException();
        }

        private void ReadFileList()
        {
            throw new NotImplementedException("Lazy...");
        }

        public XDocument GetConfig(string moduleName)
        {
            string realPath = Path.Combine(rootPath, "/config/", moduleName + ".xml");
            if (CheckInDir(realPath) == false || File.Exists(realPath) == false)
            {
                return new XDocument();
            }
            if (activeConfigs.ContainsKey(moduleName) == false)
            {
                activeConfigs[moduleName] = XDocument.Parse(File.ReadAllText(realPath));
            }
            return activeConfigs[moduleName];
        }

        public void SaveConfig(string moduleName)
        {
            if (activeConfigs.ContainsKey(moduleName) == false)
            {
                return;
            }
            string realPath = Path.Combine(rootPath, "/config/", moduleName + ".xml");
            activeConfigs[moduleName].Save(realPath);
        }
    }

    public class PluginModuleInfo
    {
        public readonly string pluginName;
        public readonly string moduleName;
        public readonly Guid moduleGuid;
        public readonly Assembly assembly;
        public readonly string fullName;
        public readonly string description;
        public readonly int version;
        public PluginModuleInfo(string pluginName, IArkDesktopPluginModule module)
        {
            this.pluginName = pluginName;
            moduleName = module.Name;
            moduleGuid = module.Guid;
            assembly = module.GetType().Assembly;
            fullName = module.GetType().FullName;
            description = module.Description;
            version = module.Version;
        }
        public static List<PluginModuleInfo> ReadFromDll(string filename)
        {
            Assembly assembly = Assembly.LoadFile(filename);
            var found = from e in assembly.GetTypes()
                        where typeof(IArkDesktopPluginModule).IsAssignableFrom(e)
                        select e;
            var ret = new List<PluginModuleInfo>();
            foreach(Type i in found)
            {
                IArkDesktopPluginModule module = (IArkDesktopPluginModule)i.Assembly.CreateInstance(i.FullName);
                ret.Add(new PluginModuleInfo(Path.GetFileName(filename), module));
            }
            return ret;
        }
    }

    public class ConfigInfo
    {
        string ConfigName { get; set; }
        Guid ConfigGuid { get; }
        string Description { get; set; } = "";
        PluginModuleInfo LaunchModule { get; set; }
        string LaunchModuleName { get; }
        Guid LaunchModuleGuid { get; }
        
        private ConfigInfo(XElement config, string dir, IEnumerable<PluginModuleInfo> modules)
        {
            ConfigName = Path.GetFileName(dir);
            ConfigGuid = new Guid(config.Element("Guid").Value);
            Description = config.Element("Description")?.Value;
            LaunchModuleName = config.Element("LaunchModule").Element("Name").Value;
            LaunchModuleGuid = new Guid(config.Element("LaunchModule").Element("Guid").Value);
            var found = from r in modules where r.moduleGuid == LaunchModuleGuid select r;
            if (found.Any()) LaunchModule = found.First();
        }

        public static ConfigInfo ReadConfigFromDisk(string dir, IEnumerable<PluginModuleInfo> modules)
        {
            if (File.Exists(Path.Combine(dir, "config.xml")) == false)
            {
                throw new ArgumentException("Invalid config directory.", nameof(dir));
            }
            XDocument document = XDocument.Load(Path.Combine(dir, "config.xml"));
            if (document.Root.Name != "ArkDesktop")
            {
                throw new Exception("Invalid config file.");
            }
            string[] requirement = new string[] { "Version", "Guid", "LaunchModule" };
            foreach(string s in requirement)
            {
                if (document.Root.Element(s) == null)
                {
                    throw new Exception("Cannot find \"" + s + "\" in config.xml.");
                }
            }
            if (int.Parse(document.Root.Element("Version").Value) != 1)
            {
                throw new Exception("Invalid version \"" + document.Root.Element("Version").Value + "\" in config.xml.");
            }
            XElement element = document.Root.Element("LaunchModule");
            if (element.Element("Name") == null || element.Element("Guid") == null)
            {
                throw new Exception("Invalid launch info in config.xml.");
            }
            return new ConfigInfo(document.Root, dir, modules);
        }
    }

    public class ConfigManager
    {
        public string rootPath;
        public List<PluginModuleInfo> Modules { get; private set; } = new List<PluginModuleInfo>();
        public List<ConfigInfo> Configs { get; private set; } = new List<ConfigInfo>();
        public ConfigManager(string root)
        {
            rootPath = root;
        }
        public void LoadDll(string filename)
        {
            Modules.AddRange(PluginModuleInfo.ReadFromDll(Path.Combine(Path.Combine(rootPath, "/plugins/"), filename)));
        }
        public void LoadConfig(string dir)
        {
            Configs.Add(ConfigInfo.ReadConfigFromDisk(Path.Combine(rootPath, dir), Modules));
        }
    }
}
