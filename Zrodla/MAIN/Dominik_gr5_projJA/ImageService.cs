﻿using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ColorToGrayScale
{
    public class ImageService : IImageService
    {
        private const int Size = 16;
        private int height;
        private int width;
        private int imageSize;

        public PixelPackage pixels { get; set; }

        public PixelPackage CopyArrayOfBitmap(PixelPackage dividedImage)
        {
            int length = dividedImage.Length;
            PixelPackage copyOfdividedImage = new PixelPackage(length);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    copyOfdividedImage.Red[i][j] = dividedImage.Red[i][j];
                    copyOfdividedImage.Green[i][j] = dividedImage.Green[i][j];
                    copyOfdividedImage.Blue[i][j] = dividedImage.Blue[i][j];
                }
            }

            return copyOfdividedImage;
        }

        public void ImageDivider(Bitmap imageToProcess)
        {
            width = imageToProcess.Width;
            height = imageToProcess.Height;
            PixelFormat pixelFormat = imageToProcess.PixelFormat;

            imageSize = width * height;
            pixels = new PixelPackage(imageSize);

            int counter = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y += Size)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        Color color = imageToProcess.GetPixel(x, i + y);
                        pixels.Red[counter][i] = color.R;
                        pixels.Green[counter][i] = color.G;
                        pixels.Blue[counter][i] = color.B;
                        counter++;
                    }
                }
            }
        }

        public Bitmap JoinIntoBigOne()
        {
            Bitmap image = new Bitmap(width, height);

            int counter = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y += Size)
                {
                    for (int i = 0; i < Size; i++)
                    {
                        Color color = Color.FromArgb(
                           pixels.Red[counter][i],
                           pixels.Green[counter][i],
                           pixels.Blue[counter][i]);

                        image.SetPixel(x, i + y, color);
                        counter++;
                    }
                }
            }
            return image;
        }
    }
}
