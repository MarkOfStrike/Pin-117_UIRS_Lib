using System;
using System.Collections.Generic;
using System.Drawing;

namespace Pin_117_UIRS_Lib_Core.ImageProcessing
{
    public static class ImageHelper
    {
        /// <summary>
        /// Цвет контура (фигуры) на изображении.
        /// </summary>
        private static readonly Color ContourColor = Color.Black;

        /// <summary>
        /// Ищет точку, которая однозначно входит в контур фигуры цветом <see cref="ContourColor"/>.
        /// Причём поиск осуществляется слева направо.
        /// </summary>
        private static Point? FindAnyContourPoint(ImageWrapper b)
        {
            var er = b.GetEnumerator();

            while (er.MoveNext())
            {
                Color cur = b[er.Current];
                if (cur.R == ContourColor.R && cur.G == ContourColor.G && cur.B == ContourColor.B)
                    return er.Current;
            }

            return null;
        }

        /// <summary>
        /// Получает контур замкнутой фигуры цвета <see cref="ContourColor"/>.
        /// </summary>
        /// <remarks>Поиск осуществляется алгоритмом левой руки.</remarks>
        /// <returns>Список точек контура по часовой стрелке.</returns>
        public static List<Point> FindContour(Bitmap bitmap)
        {
            var result = new List<Point>();

            var img = new ImageWrapper(bitmap, true);
            

            using (img)
            {
                var clockwiseOffsetPoints = new (int dx, int dy)[]
                {
                (-1, 1),
                (-1, 0),
                (-1, -1),
                (0, -1),
                (1, -1),
                (1, 0),
                (1, 1),
                (0, 1),
                };



                var startPoint = FindAnyContourPoint(img);
                if (startPoint == null) return result;

                // изначально считаем, что мы пришли в текущую точку от (-1, 1) от текущей точки
                var prevPointOffsetIndex = 0;
                var curPoint = startPoint.Value;

                do
                {
                    result.Add(curPoint);

                    var prevPointOffset = clockwiseOffsetPoints[prevPointOffsetIndex];
                    var skipOffsetIndex = Math.Abs(prevPointOffset.dx) == 1 && Math.Abs(prevPointOffset.dy) == 1
                        ? 2
                        : 3;

                    var scanStartOffsetIndex = prevPointOffsetIndex + skipOffsetIndex;

                    var found = false;
                    for (var i = 0; i < clockwiseOffsetPoints.Length; i++)
                    {
                        var curIndex = (scanStartOffsetIndex + i) % clockwiseOffsetPoints.Length;
                        var offset = clockwiseOffsetPoints[curIndex];

                        var newPointCheck = new Point(curPoint.X + offset.dx, curPoint.Y + offset.dy);
                        var pixel = img[newPointCheck.X, newPointCheck.Y];

                        if (pixel.R == ContourColor.R && pixel.G == ContourColor.G && pixel.B == ContourColor.B)
                        {
                            curPoint = newPointCheck;
                            prevPointOffsetIndex = (curIndex + 4) % clockwiseOffsetPoints.Length;

                            found = true;
                            break;
                        }
                    }

                    if (!found) break;
                } while (curPoint != startPoint);
            }

            bitmap.Dispose();

            return result;
        }
    }
}