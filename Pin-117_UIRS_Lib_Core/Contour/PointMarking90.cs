using System.Collections.Generic;
using System.Drawing;

using Pin_Library_UIRS_Core.Structs;

namespace Pin_Library_UIRS_Core
{
    public static class PointMarking90
    {
        /// <summary>
        /// Маркировка точек контура
        /// </summary>
        /// <param name="points">Список точек</param>
        /// <returns>Количество точек с кривизной +90 и –90</returns>
        public static Marking MarkContourPoints(ICollection<Point> points)
        {
            var matrix = PointsToMatrix(points);
            var countP = 0;
            var countN = 0;

            for (var x = 0; x < matrix.GetLength(0); x++)
            {
                for (var y = 0; y < matrix.GetLength(1); y++)
                {
                    if (matrix[x, y] != 0 || ((matrix[x - 1, y - 1] != 0 || matrix[x - 1, y + 1] != 0) &&
                                              (matrix[x - 1, y + 1] != 0 || matrix[x + 1, y + 1] != 0) &&
                                              (matrix[x + 1, y - 1] != 0 || matrix[x + 1, y + 1] != 0) &&
                                              (matrix[x - 1, y - 1] != 0 || matrix[x + 1, y - 1] != 0))) continue;

                    var countExternal = 0;
                    var countInternal = 0;

                    for (var k = x - 1; k <= x + 1; k++)
                    {
                        for (var l = y - 1; l <= y + 1; l++)
                        {
                            if (matrix[k, l] == 1)
                            {
                                countExternal++;
                            }

                            if (matrix[k, l] == 2)
                            {
                                countInternal++;
                            }
                        }
                    }

                    if (countExternal > countInternal)
                    {
                        countP++;
                    }
                    else
                    {
                        countN++;
                    }
                }
            }

            return new Marking(countN, countP);
        }

        /// <summary>
        /// Запись списка точек в матрицу
        /// </summary>
        /// <param name="points">Список точек</param>
        /// <returns>Матрица с исходными точками</returns>
        private static int[,] PointsToMatrix(ICollection<Point> points)
        {
            var maxX = 0;
            var maxY = 0;
            var minX = int.MaxValue;
            var minY = int.MaxValue;

            foreach (var point in points)
            {
                if (point.X > maxX)
                    maxX = point.X;

                if (point.Y > maxY)
                    maxY = point.Y;

                if (point.X < minX)
                    minX = point.X;

                if (point.Y < minY)
                    minY = point.Y;
            }

            var matrix = new int[maxX + 2, maxY + 2];

            for (var x = 0; x < matrix.GetLength(0); x++)
            {
                for (var y = 0; y < matrix.GetLength(1); y++)
                {
                    if (points.Contains(new Point(x, y)))
                    {
                        matrix[x, y] = 0;
                    }
                    else
                    {
                        matrix[x, y] = 1;
                    }
                }
            }


            var avg = new Point((minX + maxX) / 2, (minY + maxY) / 2);
            
            return FillByPoint(matrix, avg,1);
        }
        /// <summary>
        /// Заливка по контуру
        /// </summary>
        /// <param name="srs">Матрица с исходными точками</param>
        /// <param name="point">Координаты центра объекта</param>
        /// <param name="color"></param>
        /// <returns></returns>
        static int[,] FillByPoint(int[,] srs, Point point, int color = 0)
        {
            var points = new Stack<Point>();
            var numSrc = srs[point.X, point.Y];
            points.Push(point);

            
            
            while (points.Count > 0)
            {
                Point cur = points.Pop();

                if (cur.X >= 0 && cur.Y >= 0 && cur.X < srs.GetLength(0) && cur.Y < srs.GetLength(1))
                {
                    if (srs[cur.X, cur.Y] == numSrc)
                    {
                        srs[cur.X, cur.Y] = 2;
                        points.Push(new Point(cur.X + 1, cur.Y));
                        points.Push(new Point(cur.X - 1, cur.Y));
                        points.Push(new Point(cur.X, cur.Y + 1));
                        points.Push(new Point(cur.X, cur.Y - 1));
                    }
                }
            }


            return srs;
        }
        
        
    }

}