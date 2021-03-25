using System.Drawing;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

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
            CvInvoke.MedianBlur(img, img, 3);
            return img;
        }
        public static Image<Gray, byte> BinarizationImages(Image<Gray, byte> img)
        {
            CvInvoke.Threshold(img, img, 150, 255, ThresholdType.Otsu);
            return img;
        }
        public static Image<Gray, byte> DeleteTheBackground(Image<Gray, byte> img, Image<Gray, byte> bimg)
        {
            using var source = new ImageWrapper(img.ToBitmap());
            using Bitmap result = new Bitmap(source.Width, source.Height);


            using var mask = new ImageWrapper(bimg.ToBitmap());

            var en = source.GetEnumerator();

            using (var graf = Graphics.FromImage(result))
            {
                var pen = new Pen(Color.White);

                while (en.MoveNext())
                {
                    var point = en.Current;

                    var pen2 = mask[point].GetBrightness() == 1 ? new Pen(Color.White) : new Pen(source[point]);
                    graf.DrawLine(pen2, point.X, point.Y, point.X - 1, point.Y);
                }
            }

            return result.ToImage<Gray, byte>();
        }
    }
}
