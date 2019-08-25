/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ArkDesktop
{
    public class Config
    {
        public bool Empty
        {
            get
            {
                return document == null;
            }
        }

        public bool NeedSave { get; private set; } = false;

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
                    )
                )
            );
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
                    where e.Name == "Config" && e.Attributes("default").First().Value == "true"
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
            NeedSave = true;
        }

        public XElement GetElement(XName name)
        {
            if (name == "_IMPORT")
            {
                throw new Exception("Protected space");
            }
            var found = from e in config.Elements() where e.Name == name select e;
            return found.Count() == 0 ? null : found.First();
        }

        public void AppendElement(XElement element)
        {
            if (element.Name == "_IMPORT")
            {
                throw new Exception("Protected space");
            }
            config.Add(element);
        }

        public void RemoveElements(string name)
        {
            if (name == "_IMPORT")
            {
                throw new Exception("Protected space");
            }
            (from e in config.Elements()
             where e.Name == name
             select e).Remove();
        }

        public void Save(Stream stream)
        {
            document.Save(stream);
        }
    }
}
