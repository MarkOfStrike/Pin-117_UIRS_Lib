using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;

using Pin_Library_UIRS_Core;

namespace Pin_117_UIRS_Lib_Core.ImageProcessing
{
    public static class EditImage
    {
        public static Image<Gray, byte> RotateImages(Image<Gray, byte> img, double angle)
        {
            return img.Rotate(angle, new Gray(255));
        }

        public static Image<Gray, byte> ConvertToGray(Image<Bgr, byte> img)
        {
            return img.Convert<Gray, byte>();
        }
        public static Image<Gray, byte> FilterImage(Image<Gray, byte> img)
        {
            CvInvoke.MedianBlur(img, img, 1);
            return img;
        }
        public static Image<Gray, byte> BinarizationImages(Image<Gray, byte> img)
        {
            CvInvoke.Threshold(img, img, 150, 255, Emgu.CV.CvEnum.ThresholdType.Otsu);
            return img;
        }
        public static Image<Gray, byte> DeleteTheBackground(Image<Gray, byte> img, Image<Gray, byte> bimg)
        {
            using var source = new ImageWrapper(img.ToBitmap());
            using Bitmap result = new Bitmap(source.Width, source.Height);

            using var mask = new ImageWrapper(bimg.ToBitmap());

            using (var graf = Graphics.FromImage(result))
            {
                var pen = new Pen(Color.White);

                for (int i = 0; i < result.Width; i++)
                {
                    for (int j = 0; j < result.Height; j++)
                    {
                        var point = new Point(i, j);

                        var pen2 = mask[i, j].GetBrightness() == 1 ? new Pen(Color.White) : new Pen(source[i, j]);
                        graf.DrawLine(pen2, point.X, point.Y, point.X - 1, point.Y);
                    }
                }
            }

            return result.ToImage<Gray, byte>();
        }
    }
}
