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
        private ConfigManager.ManageConfigSaveMethod manageConfigSave;
        public ResourceManager(string rootPath, ConfigManager.ManageConfigSaveMethod manageConfigSave)
        {
            this.rootPath = rootPath;
            this.manageConfigSave = manageConfigSave;
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

        public string GetResRealPath(string path)
        {
            if (path.StartsWith("/") || path.StartsWith("\\")) path = path.Substring(1);
            string realPath = Path.Combine(rootPath, "resources", path);
            return realPath;
        }

        public Stream OpenRead(string filename, bool packagedFirst = false)
        {
            if (filename.StartsWith("/") || filename.StartsWith("\\"))
                filename = filename.Substring(1);
            bool packaged = false;
            if (packagedFile.ContainsKey(filename))
            {
                packaged = true;
                if (packagedFirst)
                    throw new NotImplementedException("Lazy...");
            }
            string realPath = Path.Combine(rootPath, "resources", filename);
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

        public XElement GetConfig(string moduleName)
        {
            string realPath = Path.Combine(rootPath, "config", moduleName + ".xml");
            if (CheckInDir(realPath) == false || File.Exists(realPath) == false)
            {
                var v = XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\"?><Config></Config>");
                v.Save(realPath);
            }
            if (activeConfigs.ContainsKey(moduleName) == false)
            {
                activeConfigs[moduleName] = XDocument.Parse(File.ReadAllText(realPath));
                manageConfigSave?.Invoke(realPath, activeConfigs[moduleName]);
            }
            return activeConfigs[moduleName].Root;
        }

        public void SaveConfig(string moduleName)
        {
            if (activeConfigs.ContainsKey(moduleName) == false)
            {
                return;
            }
            string realPath = Path.Combine(rootPath, "config", moduleName + ".xml");
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
            Assembly assembly;
            using (var fs = File.OpenRead(filename))
            {
                byte[] vs = new byte[fs.Length];
                int read = 0, toRead = unchecked((int)fs.Length);
                while (toRead != 0)
                {
                    int readed = fs.Read(vs, read, toRead);
                    read += readed;
                    toRead -= readed;
                }
                assembly = Assembly.Load(vs);
            }
            var found = from e in assembly.GetTypes()
                        where typeof(IArkDesktopPluginModule).IsAssignableFrom(e)
                        select e;
            var ret = new List<PluginModuleInfo>();
            foreach (Type i in found)
            {
                IArkDesktopPluginModule module = (IArkDesktopPluginModule)i.Assembly.CreateInstance(i.FullName);
                ret.Add(new PluginModuleInfo(Path.GetFileName(filename), module));
            }
            return ret;
        }
    }

    public class ConfigInfo
    {
        public bool disableSave = true;
        /// <summary>
        /// Change before launch!
        /// </summary>
        public string ConfigName { get => configName; set { configName = value; Save(); } }
        public Guid ConfigGuid { get; }
        public string Description { get => description; set { description = value; Save(); } }
        public PluginModuleInfo LaunchModule
        {
            get => launchModule;
            set
            {
                launchModule = value;
                LaunchModuleName = value.moduleName;
                LaunchModuleGuid = value.moduleGuid;
                Save();
            }
        }
        public string LaunchModuleName { get; private set; }
        public Guid LaunchModuleGuid { get; private set; }
        public string rootPath;
        private string configName;
        private string description;
        private PluginModuleInfo launchModule;
        private ConfigManager.ManageConfigSaveMethod manageConfigSave;

        private void Save()
        {
            if (disableSave) return;
            XDocument document = new XDocument(
                new XElement("ArkDesktop",
                    new XElement("Version", 1),
                    new XElement("Guid", ConfigGuid.ToString()),
                    new XElement("Description", description),
                    new XElement("LaunchModule",
                        new XElement("Name", LaunchModuleName),
                        new XElement("Guid", LaunchModuleGuid))));
            document.Save(Path.Combine(rootPath, "config.xml"));
            if (Path.GetFileName(rootPath) != configName)
            {
                Directory.Move(rootPath, Path.Combine(Path.GetDirectoryName(rootPath), configName));
                rootPath = Path.Combine(Path.GetDirectoryName(rootPath), configName);
            }
        }
        public void LoadPluginModule(PluginModuleInfo module)
        {
            launchModule = module;
        }

        private ConfigInfo(XElement config, string dir, IEnumerable<PluginModuleInfo> modules, ConfigManager.ManageConfigSaveMethod manageConfigSave)
        {
            rootPath = dir;
            this.manageConfigSave = manageConfigSave;
            ConfigName = Path.GetFileName(dir);
            ConfigGuid = new Guid(config.Element("Guid").Value);
            Description = config.Element("Description")?.Value;
            LaunchModuleName = config.Element("LaunchModule").Element("Name").Value;
            LaunchModuleGuid = new Guid(config.Element("LaunchModule").Element("Guid").Value);
            var found = from r in modules where r.moduleGuid == LaunchModuleGuid select r;
            if (found.Any()) LoadPluginModule(found.First());
            disableSave = false;
        }

        public static ConfigInfo ReadConfigFromDisk(string dir, IEnumerable<PluginModuleInfo> modules, ConfigManager.ManageConfigSaveMethod manageConfigSave)
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
            foreach (string s in requirement)
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
            return new ConfigInfo(document.Root, dir, modules, manageConfigSave);
        }

        public ResourceManager ResourceManager => new ResourceManager(rootPath, manageConfigSave);
    }

    public class ConfigManager
    {
        public string rootPath;
        public List<PluginModuleInfo> Modules { get; private set; } = new List<PluginModuleInfo>();
        public List<ConfigInfo> Configs { get; private set; } = new List<ConfigInfo>();
        public List<Tuple<string, XDocument>> managedConfigSave = new List<Tuple<string, XDocument>>();
        private HashSet<Guid> loadedConfigs = new HashSet<Guid>();
        private HashSet<Guid> loadedModules = new HashSet<Guid>();

        public ConfigManager(string root)
        {
            rootPath = root;
        }
        public void LoadDll(string filename)
        {
            foreach (var i in PluginModuleInfo.ReadFromDll(Path.Combine(Path.Combine(rootPath, "/plugins/"), filename)))
            {
                if (loadedModules.Contains(i.moduleGuid) == false)
                {
                    loadedModules.Add(i.moduleGuid);
                    Modules.Add(i);
                    foreach (var j in from e in Configs where e.LaunchModule == null && e.LaunchModuleGuid == i.moduleGuid select e)
                        j.LoadPluginModule(i);
                }
            }
        }
        public void LoadConfig(string dir)
        {
            var get = ConfigInfo.ReadConfigFromDisk(Path.Combine(rootPath, dir), Modules, ManageConfigSave);
            if (loadedConfigs.Contains(get.ConfigGuid) == false)
            {
                loadedConfigs.Add(get.ConfigGuid);
                Configs.Add(get);
            }
        }

        public void ScanPlugins(string dir)
        {
            var list = Directory.EnumerateFiles(dir);
            foreach (var i in list)
            {
                if (i.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        LoadDll(i);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        public void ScanPlugins() => ScanPlugins(Path.Combine(rootPath, "plugins"));

        public void ScanConfigs(string dir)
        {
            if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
            foreach (var i in Directory.EnumerateDirectories(dir, "*", SearchOption.TopDirectoryOnly))
            {
                if (i.StartsWith("__shared") == false)
                {
                    try
                    {
                        LoadConfig(i);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        public void ScanConfigs() => ScanConfigs(Path.Combine(rootPath, "configs"));

        public void SaveAllConfigs()
        {
            foreach (var i in managedConfigSave) i.Item2.Save(i.Item1);
        }

        public delegate void ManageConfigSaveMethod(string path, XDocument document);
        public void ManageConfigSave(string path, XDocument document)
        {
            managedConfigSave.Add(new Tuple<string, XDocument>(path, document));
        }
    }
}
