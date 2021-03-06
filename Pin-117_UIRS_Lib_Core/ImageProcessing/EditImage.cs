using System.Drawing;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Pin_117_UIRS_Lib_Core.ImageProcessing
{
    public static class EditImage
    {
        public static Image<Gray, byte> RotateImages(Image<Gray, byte> img, double angle) => img.Rotate(angle, new Gray(255));
        public static Image<Gray, byte> ConvertToGray(Image<Bgr, byte> img) => img.Convert<Gray, byte>();
        public static void FilterImage(Image<Gray, byte> img) => CvInvoke.MedianBlur(img, img, 3);
        public static void BinarizationImages(Image<Gray, byte> img) => CvInvoke.Threshold(img, img, 150, 255, ThresholdType.Otsu);
        public static Image<Gray, byte> DeleteTheBackground(Image<Gray, byte> img, Image<Gray, byte> bimg)
        {
            using var source = new ImageWrapper(img.ToBitmap());
            using Bitmap result = new Bitmap(source.Width, source.Height);

            using var mask = new ImageWrapper(bimg.ToBitmap());

            var en = source.GetEnumerator();

            while (en.MoveNext())
            {
                var point = en.Current;

                var color = mask[point].GetBrightness() == 1 ? Color.White : source[point];

                result.SetPixel(point.X, point.Y, color);
            }

            return result.ToImage<Gray, byte>();
        }
    }
}
