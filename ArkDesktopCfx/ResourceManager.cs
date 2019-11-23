using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chromium;
using System.Web;

namespace ArkDesktopCfx
{
    namespace ResourceManager
    {
        class SchemeHandlerFactory : CfxSchemeHandlerFactory
        {
            string basePath;
            public SchemeHandlerFactory(string basePath)
            {
                this.basePath = basePath;
                Create += SchemeHandlerFactory_Create;
            }

            private void SchemeHandlerFactory_Create(object sender, Chromium.Event.CfxCreateEventArgs e)
            {
                if (!e.Request.Url.StartsWith("akd://test/"))
                {
                    throw new Exception("Not akd");
                }
                string file = Path.Combine(basePath, e.Request.Url.Substring("akd://test/".Length));
                if (File.Exists(file) == false)
                {
                    using (MemoryStream ms = new MemoryStream())
                        e.SetReturnValue(new ArkDesktopBrowserControl.ResourceHandler("text/plain", ms));
                }
                else
                {
                    e.SetReturnValue(new ArkDesktopBrowserControl.ResourceHandler(MimeMapping.GetMimeMapping(file), File.OpenRead(file)));
                }
            }
        }
    }
}
