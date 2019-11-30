﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ColorToGrayScale
{
    public class CppDll : IDll
    {
        public ProcessingMethodDelegate ProcessingMethod { internal get; set; }

        public void ColorChanger(byte[] r, byte[] g, byte[] b) => ColorChange(r, g, b);

        [DllImport(@"C:\Users\qwertyuiop\Desktop\repo\Zrodla\DLL_C\x64\Release\C_DLL.dll")]
        private static extern void ColorChange(byte[] r, byte[] g, byte[] b);

        public void ChangeColorToGrayScale(object data)
        {
            Bitmap[] image = (Bitmap[])data;

            int i = ThreadService.GetI();

            while (i < image.Length)
            {
                PixelPackage pixels = new PixelPackage();
                pixels.Set(image[i]);

                ProcessingMethod(pixels.R, pixels.G, pixels.B);

                //ColorChange(pixels.R, pixels.G, pixels.B);

                image[i] = pixels.Get();

                i = ThreadService.GetI();
            }
        }
    }
}
