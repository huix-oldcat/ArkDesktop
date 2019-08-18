/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ImageStitchingTool
{
    class Program
    {
        static List<Image> frames = new List<Image>();
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ImageStitchingTool pic1 pic2 [pic3 ...]");
            }
            else
            {
                int width = -1, height = -1;
                bool failed = false;
                foreach (string path in args)
                {
                    frames.Add(Image.FromFile(path));
                    Console.WriteLine(string.Format("Info: Read image from {0} . W={1} H={2}", path, frames[frames.Count - 1].Width.ToString(), frames[frames.Count - 1].Height.ToString()));
                    if (width == -1)
                    {
                        width = frames[frames.Count - 1].Width;
                        height = frames[frames.Count - 1].Height;
                    }
                    else if (width != frames[frames.Count - 1].Width || height != frames[frames.Count - 1].Height)
                    {
                        Console.WriteLine("Error: The height or width of this image is different from the previous ones.");
                        failed = true;
                        break;
                    }
                }
                if (!failed)
                {
                    Make();
                }
            }
            Console.ReadLine();
        }

        static void Make()
        {
            Bitmap bitmap = new Bitmap(frames[0].Width * frames.Count, frames[0].Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            for (int i = 0; i < frames.Count; ++i)
            {
                graphics.DrawImage(frames[i], new Point(i * frames[0].Width, 0));
            }
            graphics.Dispose();
            string name = "output-" + Guid.NewGuid() + ".png";
            bitmap.Save(name, System.Drawing.Imaging.ImageFormat.Png);
            Console.WriteLine("Info :File saved to " + name);
        }
    }
}
