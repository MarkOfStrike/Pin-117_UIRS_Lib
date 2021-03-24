using System;
using System.Collections.Generic;
using System.Drawing;

using Pin_117_UIRS_Lib_Core.ImageProcessing;
using Pin_117_UIRS_Lib_Core.Structs.Interfaces;

using Pin_Library_UIRS_Core;

namespace Pin_117_UIRS_Lib_Core.Signs
{
    public static class Calculation
    {
        public static List<Point> GetPoint(Bitmap sourse, bool smoothing = true)
        {
            var result = ImageHelper.FindContour(sourse);

            return smoothing ? ConturSmoother.FilterContour(result) : result;

        }

        public static int GetS0(Bitmap source)
        {
            using var img = new ImageWrapper(source);
            var en = img.GetEnumerator();

            int s0 = 0;

            while (en.MoveNext())
            {
                if (img[en.Current].ToArgb() == Color.Black.ToArgb())
                {
                    s0++;
                }
            }

            return s0;
        }

        /// <summary>
        /// Основной метод для получения признаков по варианту
        /// </summary>
        /// <param name="points"></param>
        /// <param name="s0"></param>
        /// <param name="p0"></param>
        /// <returns></returns>
        public static double[] GetOdds(List<Point> points, int s0, int p0)
        {
            int a = 1;
            double b = 1.414;

            var KT = CountingAlgoritm.CountPoints(points);

            int k = KT.K;
            int t = KT.T;

            var m12 = (IMarking)PointMarking90.MarkContourPoints(points);
            int m1 = m12.CountNegativeAngles;
            int m2 = m12.CountPositiveAngles;

            var m34 = (IMarking)PointMarking135.Build(points);
            int m3 = m34.CountNegativeAngles;
            int m4 = m34.CountPositiveAngles;


            var Lln = 0.5d * (k * 2d * a + (double)t * 2 * b);
            var Lvp = 0.5d * (m1 * 2d * b + m3 * (a + b));
            var Lvg = 0.5d * (m2 * 2d * b + m4 * (a + b));

            var k1 = m1 / (double)(m1 + m2 + m3 + m4);
            var k2 = m2 / (double)(m1 + m2 + m3 + m4);
            var k3 = m3 / (double)(m1 + m2 + m3 + m4);
            var k4 = m4 / (double)(m1 + m2 + m3 + m4);
            var k5 = Lln / (Lln + Lvp + Lvg);
            var k6 = Lvp / (Lln + Lvp + Lvg);
            var k7 = Lvg / (Lln + Lvp + Lvg);
            var k8 = (m1 + m2 + m3 + m4) / (double)(p0 + s0 + k + t);
            var k9 = (k + t) / (double)(p0 + s0);

            return new double[] { k1, k2, k3, k4, k5, k6, k7, k8, k9 };
        }

        //ДОДЕЛАТЬ ВАРИАНТЫ

        private static void ForOne() { }
        private static void ForTwo() { }
        private static void ForTree() { }
        private static void ForFour() { }
        private static void ForFive() { }
        private static void ForSix() { }
        private static void ForSeven() { }

        private static void test()
        {
            int a = 0;

            var ac = new Func<List<Point>, int, int, double[]>(GetOdds);


        }
    }
}
