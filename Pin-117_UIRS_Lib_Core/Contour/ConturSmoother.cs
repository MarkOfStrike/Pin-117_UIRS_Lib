using System.Collections.Generic;
using System.Drawing;

using Pin_Library_UIRS_Core.Structs;

namespace Pin_Library_UIRS_Core
{
    public static class ConturSmoother
    {
        //Список паттернов, используемых для анализа окружения точек контура
        static ICollection<Pattern> GetPattern => new Pattern[]
        {
            new Pattern(new Point{X = -1,Y = -1 }, new Point{X = 1,Y = -1 }),
            new Pattern(new Point{X = 1,Y = -1 }, new Point{X = 1,Y = 1 }),
            new Pattern(new Point{X = -1,Y = 1 }, new Point{X = 1,Y = 1 }),
            new Pattern(new Point{X = -1,Y = -1 }, new Point{X = -1,Y = 1 }),
            new Pattern(new Point{X = 0,Y = -1 }, new Point{X = 0,Y = 1 }),
            new Pattern(new Point{X = 1,Y = 0 }, new Point{X = -1,Y = 0 }),
            new Pattern(new Point{X = 1,Y = -1 }, new Point{X = -1,Y = 1 }),
            new Pattern(new Point{X = -1,Y = -1 }, new Point{X = 1,Y = 1 }),
            new Pattern(new Point{X = 0,Y = -1 }, new Point{X = 1,Y = 1 }),
            new Pattern(new Point{X = 1,Y = -1 }, new Point{X = 0,Y = 1 }),
            new Pattern(new Point{X = 1,Y = 0 }, new Point{X = -1,Y = 1 }),
            new Pattern(new Point{X = -1,Y = 0 }, new Point{X = 1,Y = 1 }),
            new Pattern(new Point{X = -1,Y = -1 }, new Point{X = 0,Y = 1 }),
            new Pattern(new Point{X = 0,Y = -1 }, new Point{X = -1,Y = 1 }),
            new Pattern(new Point{X = 1,Y = -1 }, new Point{X = -1,Y = 0 }),
            new Pattern(new Point{X = -1,Y = -1 }, new Point{X = 1,Y = 0 }),
        };

        public static List<Point> FilterContour(ICollection<Point> defaultContour)
        {
            var result = new List<Point>();

            var patterns = GetPattern;

            // Обход точек контура
            foreach (var a in defaultContour)
            {
                // Проверяем окружение точки на соответствие паттернам
                foreach (var pattern in patterns)
                {
                    if (defaultContour.Contains(new Point { X = a.X + pattern.FirstNeighbor.Y, Y = a.Y + pattern.FirstNeighbor.X }) &&
                        defaultContour.Contains(new Point { X = a.X + pattern.SecondNeighbor.Y, Y = a.Y + pattern.SecondNeighbor.X }))
                    {
                        // Точки, находящихся в участках, требующих сглаживания, могут соответствовать одновременно нескольким паттернам
                        // Поэтому размещаем следующее условие, проверяющее, значится ли точка в результирующем списке
                        if (!result.Contains(a)) result.Add(a);
                    }
                }
            }
            return result;
        }
    }

}
