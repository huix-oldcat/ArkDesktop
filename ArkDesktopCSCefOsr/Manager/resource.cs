/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ArkDesktopCSCefOsr
{
    static public partial class Manager
    {
        static public class Resources
        {
            public class Resource
            {
                public readonly string srcUrl, destPath, mimeType;

                public Resource(string srcUrl, string destPath, string mimeType)
                {
                    this.srcUrl = srcUrl;
                    this.destPath = destPath;
                    this.mimeType = mimeType;
                }
            }
            public static Dictionary<string, Resource> Redirects { get; private set; } = new Dictionary<string, Resource>();
            public static void LoadConfig(string path)
            {
                XmlDocument document = new XmlDocument();
                document.Load(path);
                XmlNode root = document.SelectSingleNode("ArkDesktop");
                XmlNode versionNode = root.SelectSingleNode("Version");
                if(versionNode == null)
                {
                    throw new Exception("Config version not found.");
                }
                if (versionNode.InnerText != "0.1")
                {
                    throw new Exception("Config version not match.");
                }
                foreach (XmlNode node in root.ChildNodes)
                {
                    switch(node.Name)
                    {
                        case "Redirect":
                            XmlElement element = (XmlElement)node;
                            if (element.HasAttribute("MimeType") == false)
                            {
                                element.SetAttribute("MimeType", "text/plain");
                            }
                            if(element.HasAttribute("Src") == false || element.HasAttribute("Desc") == false)
                            {
                                throw new Exception("Config format not recognized");
                            }
                            Redirects.Add(element.GetAttribute("Src"), new Resource(
                                element.GetAttribute("Src"),
                                element.GetAttribute("Desc"),
                                element.GetAttribute("MimeType")));
                            break;
                    }
                }
            }
        }
    }
}
