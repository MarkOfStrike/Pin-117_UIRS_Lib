using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Pin_Library_UIRS_Core.Structs;

namespace Pin_Library_UIRS_Core
{
    public static class PointMarking135
    {
        private static int[,] FillByPoint(int[,] srs, Point point, int color)
        {
            Stack<Point> points = new Stack<Point>();
            int numSrc = srs[point.X, point.Y];
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
        

        private static int[,] BuildPoint(IList<Point> pointses)
        {
            int X = pointses.Max(s => s.X);
            int Y = pointses.Max(s => s.Y);

            int[,] allMas = new int[X + 6, Y + 6];

            for (int i = 0; i < allMas.GetLength(0); i++)
            {
                for (int j = 0; j < allMas.GetLength(1); j++)
                {
                    allMas[i, j] = 1;

                }
            }

            foreach (var point in pointses)
            {
                allMas[point.X, point.Y] = 0;
            }

            var avg = new Point(allMas.GetLength(0) / 2, allMas.GetLength(1) / 2);

            return FillByPoint(allMas, avg, 1);
            
        }

        public static Marking Build(ICollection<Point> pointses)
        {
            var mas = BuildPoint(pointses.ToList());

            var minus = 0;
            var plus = 0;
            var pointPlus = new List<Point>();
            var pointMinus = new List<Point>();

            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if (mas[i, j] == 0 && ((mas[i - 1, j] == 0 && mas[i + 1, j + 1]
                   == 0) || (mas[i + 1, j] == 0 && mas[i - 1, j + 1] == 0) || (mas[i, j + 1] == 0 && mas[i + 1, j - 1] == 0) || (mas[i, j - 1] == 0 && mas[i + 1, j + 1] == 0) || (mas[i - 1, j - 1] == 0 && mas[i -
        1, j] == 0) || (mas[i + 1, j - 1] == 0 && mas[i - 1, j] == 0) || (mas[i, j - 1] == 0 && mas[i -
        1, j + 1] == 0) || (mas[i - 1, j - 1] == 0 && mas[i, j + 1] == 0)))
                    {
                        int externalCount = 0;
                        int internalCount = 0;

                        for (int k = i - 1; k <= i + 1; k++)
                        {
                            for (int l = j - 1; l <= j + 1; l++)
                            {
                                if (mas[k, l] == 1)
                                {
                                    externalCount++;
                                }

                                if (mas[k, l] == 2)
                                {
                                    internalCount++;
                                }
                            }
                        }

                        if (externalCount > internalCount)
                        {
                            plus++;
                            pointPlus.Add(new Point(i, j));
                        }
                        else
                        {
                            minus++;
                            pointMinus.Add(new Point(i, j));
                        }
                    }

                }
            }

            return new Marking(minus, plus, pointPlus,pointMinus);
        }


    }
}