/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ArkDesktop
{
    public class Config
    {
        public class ConfigInfo
        {
            public readonly string configName;
            public readonly bool isDefault;

            public ConfigInfo(string configName, bool isDefault)
            {
                this.configName = configName;
                this.isDefault = isDefault;
            }
        }
        public bool Empty
        {
            get
            {
                return document == null;
            }
        }

        public bool NeedSave { get; private set; } = false;
        public ConfigInfo[] ConfigList
        {
            get
            {
                return (from e in document.Root.Elements()
                        where e.Name == "Config"
                        select new ConfigInfo(e.Attribute("name").Value, e.Attribute("default")?.Value == "true")).ToArray();
            }
        }

        public string[] PluginList
        {
            get
            {
                return (from e in document.Root.Element("Plugins").Elements()
                        where e.Name == "Plugin"
                        select e.Value).ToArray();
            }
        }

        private XDocument document = null;
        private XElement config;

        public void Create()
        {
            document = new XDocument(
                new XDeclaration("1.0", "UTF-8", null),
                new XElement("ArkDesktop",
                    new XElement("Version", "0.2"),
                    new XElement("Config",
                        new XAttribute("name", "default"),
                        new XAttribute("default", "true")
                    ),
                    new XElement("Plugins")
                )
            );
            config = document.Root.Element("Config");
            document.Changed += Document_Changed;
        }

        public void Load(Stream stream)
        {
            document = XDocument.Load(stream);
            if (document == null)
            {
                throw new Exception("Not a valid XML document.");
            }
            if (document.Root.Name != "ArkDesktop")
            {
                throw new Exception("Not a valid ArkDesktop config file.");
            }
            var found = from e in document.Root.Elements()
                        where e.Name == "Version"
                        select e;
            if (found.Count() == 0)
            {
                throw new Exception("Not a valid ArkDesktop config file.");
            }
            var version = found.First();
            if (version.Value != "0.2")
            {
                throw new Exception("The version of this file is not supported.");
            }
            found = from e in document.Root.Elements()
                    where e.Name == "Config" && e.Attribute("default")?.Value == "true"
                    select e;
            if (found.Count() != 0)
            {
                config = found.First();
            }
            else
            {
                found = from e in document.Root.Elements()
                        where e.Name == "Config"
                        select e;
                if (found.Count() == 0)
                {
                    throw new Exception("This file doesn't contains any config.");
                }
                config = found.First();
            }
            document.Changed += Document_Changed;
        }

        private void Document_Changed(object sender, XObjectChangeEventArgs e)
        {
            XElement th = sender as XElement;
            if(th == null)
            {
                return;
            }
            if(th.Name == "_LAUNCH")
            {
                th.Name = "LAUNCH_";
                throw new Exception("Protected space");
            }
            NeedSave = true;
        }

        public XElement GetElement(XName name)
        {
            if (name == "_LAUNCH")
            {
                throw new Exception("Protected space");
            }
            var found = from e in config.Elements() where e.Name == name select e;
            if(!found.Any())
            {
                return null;
            }
            return found.First();
        }

        internal void ChangeDefaultConfig(string name)
        {
            var found = from e in document.Root.Elements() where e.Name == "Config" && e.Attribute("name").Value == name select e;
            if (!found.Any())
            {
                throw new Exception("Config not exist.");
            }
            foreach (XElement i in from e in document.Root.Elements() where e.Name == "Config" && e.Attribute("default")?.Value == "true" select e)
            {
                i.Attribute("default").Remove();
            }
            found.First().Add(new XAttribute("default", "true"));
        }

        internal void CopyTo(string destName)
        {
            if (GetElement(destName) != null)
            {
                throw new Exception("Dest config exists.");
            }
            XElement element = new XElement(config);
            element.Attribute("name").Value = destName;
            element.Attribute("default")?.Remove();
            document.Root.Add(element);
        }

        public void AppendElement(XElement element)
        {
            if (element.Name == "_LAUNCH")
            {
                throw new Exception("Protected space");
            }
            config.Add(element);
        }

        public void RemoveElements(string name)
        {
            if (name == "_LAUNCH")
            {
                throw new Exception("Protected space");
            }
            (from e in config.Elements()
             where e.Name == name
             select e).Remove();
        }

        public string GetLaunchPlugin()
        {
            return config.Element("_LAUNCH")?.Value;
        }

        internal void SetLaunchPlugin(string pluginName)
        {
            (from e in config.Elements() where e.Name == "_LAUNCH" select e).Remove();
            config.Add(new XElement("_LAUNCH", pluginName));
        }

        internal bool IsDefaultConfig()
        {
            return (config.Attribute("default")?.Value == "true") == true;
        }

        public void Save(Stream stream)
        {
            document.Save(stream);
        }

        internal void AddPlugin(string pluginName)
        {
            if ((from e in document.Root.Element("Plugins").Elements() where e.Name == "Plugin" && e.Value == pluginName select e).Any() == false)
            {
                document.Root.Element("Plugins").Add(new XElement("Plugin", pluginName));
            }
        }

        internal void RemovePlugin(string pluginName)
        {
            (from e in document.Root.Element("Plugins").Elements() where e.Name == "Plugin" && e.Value == pluginName select e).Remove();
        }

        internal void RenameConfig(string newName)
        {
            if ((from e in document.Root.Elements() where e.Name == "Config" && e.Attribute("name").Value == newName select e).Any())
            {
                throw new Exception("Config exists.");
            }
            config.Attribute("name").Value = newName;
        }

        public void ChangeActiveConfig(string configName)
        {
            var found = from e in document.Root.Elements() where e.Name == "Config" && e.Attribute("name").Value == configName select e;
            if (found.Any() == false)
            {
                throw new Exception("No such config");
            }
            config = found.First();
        }

        internal void RemoveConfig()
        {
            config.Remove();
            var found = from e in document.Root.Elements() where e.Name == "Config" select e;
            var defaultConfig = from e in found where e.Attribute("default")?.Value == "true" select e;
            if (defaultConfig.Any())
            {
                config = defaultConfig.First();
            }
            else
            {
                config = found.First();
                config.Add(new XAttribute("default", "true"));
            }
        }
    }
}
