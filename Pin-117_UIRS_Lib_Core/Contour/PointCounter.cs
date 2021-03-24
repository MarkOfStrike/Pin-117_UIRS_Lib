using System.Collections.Generic;
using System.Drawing;

namespace Pin_Library_UIRS_Core
{
    public static class PointCounter
    {

        public static int[] ToCountPoints(List<Point> points)
        {
            int[] orintations = new int[8];

            for (int n = 0; n < orintations.Length; n++)
            {
                orintations[n] = 0;
            }

            for (int i = 0; i < points.Count; i++)
            {
                if (i != points.Count - 1) //not last point
                {
                    if (points[i + 1].X == points[i].X && points[i + 1].Y == points[i].Y + 1)// orientation 1
                    {
                        orintations[0]++;
                    }
                    if (points[i + 1].X == points[i].X - 1 && points[i + 1].Y == points[i].Y + 1)// orientation 2
                    {
                        orintations[1]++;
                    }
                    if (points[i + 1].X == points[i].X - 1 && points[i + 1].Y == points[i].Y)// orientation 3
                    {
                        orintations[2]++;
                    }
                    if (points[i + 1].X == points[i].X - 1 && points[i + 1].Y == points[i].Y - 1)// orientation 4
                    {
                        orintations[3]++;
                    }
                    if (points[i + 1].X == points[i].X && points[i + 1].Y == points[i].Y - 1)// orientation 5
                    {
                        orintations[4]++;
                    }
                    if (points[i + 1].X == points[i].X + 1 && points[i + 1].Y == points[i].Y - 1)// orientation 6
                    {
                        orintations[5]++;
                    }
                    if (points[i + 1].X == points[i].X + 1 && points[i + 1].Y == points[i].Y)// orientation 7
                    {
                        orintations[6]++;
                    }
                    if (points[i + 1].X == points[i].X + 1 && points[i + 1].Y == points[i].Y + 1)// orientation 8
                    {
                        orintations[7]++;
                    }
                    if (points[i + 1].X - points[i].X > 1 || points[i + 1].X - points[i].X < -1 || points[i + 1].Y - points[i].Y > 1 || points[i + 1].Y - points[i].Y < -1)
                    {
                        throw new System.Exception("choto s koordinatami ne tak");
                    }
                }
                else //last point
                {
                    if (points[0].X == points[i].X && points[0].Y == points[i].Y + 1)// orientation 1
                    {
                        orintations[0]++;
                    }
                    if (points[0].X == points[i].X - 1 && points[0].Y == points[i].Y + 1)// orientation 2
                    {
                        orintations[1]++;
                    }
                    if (points[0].X == points[i].X - 1 && points[0].Y == points[i].Y)// orientation 3
                    {
                        orintations[2]++;
                    }
                    if (points[0].X == points[i].X - 1 && points[0].Y == points[i].Y - 1)// orientation 4
                    {
                        orintations[3]++;
                    }
                    if (points[0].X == points[i].X && points[0].Y == points[i].Y - 1)// orientation 5
                    {
                        orintations[4]++;
                    }
                    if (points[0].X == points[i].X + 1 && points[0].Y == points[i].Y - 1)// orientation 6
                    {
                        orintations[5]++;
                    }
                    if (points[0].X == points[i].X + 1 && points[0].Y == points[i].Y)// orientation 7
                    {
                        orintations[6]++;
                    }
                    if (points[0].X == points[i].X + 1 && points[0].Y == points[i].Y + 1)// orientation 8
                    {
                        orintations[7]++;
                    }
                    if (points[0].X - points[i].X > 1 || points[0].X - points[i].X < -1 || points[0].Y - points[i].Y > 1 || points[0].Y - points[i].Y < -1)
                    {
                        throw new System.Exception("choto s koordinatami ne tak");
                    }

                }


            }

            return orintations;

        }

    }
}

