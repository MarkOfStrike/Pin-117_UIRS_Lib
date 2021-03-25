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
        private const int A = 1;
        private const double B = 1.414;

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
        /// <param name="points">Список точек контура</param>
        /// <param name="s0">Площадь объекта</param>
        /// <param name="p0">Периметр объекта(кол-во точек контура)</param>
        /// <param name="numTask">Номер задания</param>
        /// <returns>Набор безразмерных признаков</returns>
        public static double[] GetOdds(List<Point> points, int s0, int p0, int numTask) => numTask switch
        {
            1 => ForOne(points, s0, p0),
            2 => ForTwo(points, s0, p0),
            3 => ForTree(points, s0, p0),
            4 => ForFour(points, p0),
            5 => ForFive(points, s0, p0),
            6 => ForSix(points, s0, p0),
            7 => ForSeven(points, s0, p0),

            _ => throw new NotImplementedException("Такого варианта не существует!")
        };

        //ДОДЕЛАТЬ ВАРИАНТЫ

        private static double[] ForOne(List<Point> points, int s0, int p0) { throw new NotImplementedException(); }
        private static double[] ForTwo(List<Point> points, int s0, int p0) { throw new NotImplementedException(); }
        private static double[] ForTree(List<Point> points, int s0, int p0) 
        {
            var KT = CountingAlgoritm.CountPoints(points);

            int k = KT.K;
            int t = KT.T;

            var m12 = (IMarking)PointMarking90.MarkContourPoints(points);
            int m1 = m12.CountNegativeAngles;
            int m2 = m12.CountPositiveAngles;

            var m34 = (IMarking)PointMarking135.Build(points);
            int m3 = m34.CountNegativeAngles;
            int m4 = m34.CountPositiveAngles;


            var k1 = points.Count / (double)s0;
            var k2 = (double)m1 / (double)s0;
            var k3 = (double)m2 / (double)s0;
            var k4 = (double)m3 / (double)s0;
            var k5 = (double)m4 / (double)s0;
            var k6 = (double)k / (double)s0;
            var k7 = (double)t / (double)s0;
            var k8 = (double)m1 / (double)p0;
            var k9 = (double)m2 / (double)p0;
            var k10 = (double)m3 / (double)p0;
            var k11 = (double)m4 / (double)p0;

            return new double[] { k1, k2, k3, k4, k5, k6, k7, k8, k9, k10, k11 };
        }
        private static double[] ForFour(List<Point> points, int p0) 
        {
            var KT = CountingAlgoritm.CountPoints(points);
            int k = KT.K;
            int t = KT.T;

            var m12 = (IMarking)PointMarking90.MarkContourPoints(points);
            int m1 = m12.CountNegativeAngles;
            int m2 = m12.CountPositiveAngles;

            var m34 = (IMarking)PointMarking135.Build(points);
            int m3 = m34.CountNegativeAngles;
            int m4 = m34.CountPositiveAngles;

            var orientations = PointCounter.ToCountPoints(points);

            var Lln = 0.5d * (k * 2d * A + (double)t * 2 * B);
            var Lvp = 0.5d * (m1 * 2d * B + m3 * (A + B));
            var Lvg = 0.5d * (m2 * 2d * B + m4 * (A + B));
            var Lkont = GetLkont(orientations);

            double k1 = m1 / p0;
            double k2 = m2 / p0;
            double k3 = m3 / p0;
            double k4 = m4 / p0;
            double k5 = k / p0;
            double k6 = t / p0;
            double k7 = Lln / Lkont;
            double k8 = Lvg / Lkont;
            double k9 = Lvp / Lkont;

            return new double[] { k1, k2, k3, k4, k5, k6, k7, k8, k9 };

        }
        private static double[] ForFive(List<Point> points, int s0, int p0) 
        {
            var KT = CountingAlgoritm.CountPoints(points);

            int k = KT.K;
            int t = KT.T;

            var m12 = (IMarking)PointMarking90.MarkContourPoints(points);
            int m1 = m12.CountNegativeAngles;
            int m2 = m12.CountPositiveAngles;

            var m34 = (IMarking)PointMarking135.Build(points);
            int m3 = m34.CountNegativeAngles;
            int m4 = m34.CountPositiveAngles;


            var k1 = m1 / (double)(m1 + m2 + m3 + m4);
            var k2 = m2 / (double)(m1 + m2 + m3 + m4);
            var k3 = m3 / (double)(m1 + m2 + m3 + m4);
            var k4 = m4 / (double)(m1 + m2 + m3 + m4);
            var k5 = (double)(m1 + m2 + m3 + m4) / (double)(p0 + s0 + k + t);
            var k6 = (double)(k + t) / (double)(p0 + s0);
            var k7 = (double)(m1 + m2 + m3 + m4) / (double)(p0 + s0);
            var k8 = (double)((m1 + m2 + m3 + m4) * k) / (p0 * s0);
            var k9 = (double)((m1 + m2 + m3 + m4) * t) / (p0 * s0);

            return new double[] { k1, k2, k3, k4, k5, k6, k7, k8, k9 };
        }
        private static double[] ForSix(List<Point> points, int s0, int p0) 
        {
            var KT = CountingAlgoritm.CountPoints(points);
            int k = KT.K;
            int t = KT.T;

            var m12 = (IMarking)PointMarking90.MarkContourPoints(points);
            int m1 = m12.CountNegativeAngles;
            int m2 = m12.CountPositiveAngles;

            var m34 = (IMarking)PointMarking135.Build(points);
            int m3 = m34.CountNegativeAngles;
            int m4 = m34.CountPositiveAngles;

            var Lln = 0.5d * (k * 2d * A + (double)t * 2 * B);
            var Lvp = 0.5d * (m1 * 2d * B + m3 * (A + B));
            var Lvg = 0.5d * (m2 * 2d * B + m4 * (A + B));

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
        private static double[] ForSeven(List<Point> points, int s0, int p0) { throw new NotImplementedException(); }


        private static double GetLkont(int[] val) => (1 * (val[0] + val[2] + val[4] + val[6]) + Math.Sqrt(val[1] + val[3] + val[5] + val[6]));
    }
}
