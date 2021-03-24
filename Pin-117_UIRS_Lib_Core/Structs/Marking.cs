using System.Collections.Generic;
using System.Drawing;
using Pin_117_UIRS_Lib_Core.Structs.Interfaces;

namespace Pin_Library_UIRS_Core.Structs
{
    public struct Marking : IMarking
    {
        public int CountPositiveAngles { get; }
        public int CountNegativeAngles { get; }

        public ICollection<Point> PositivePoints { get; }
        public ICollection<Point> NegativePoints { get; }

        public Marking(int minus, int plus) : this()
        {
            CountNegativeAngles = minus;
            CountPositiveAngles = plus;
            PositivePoints = new List<Point>();
            NegativePoints = new List<Point>();
        }

        public Marking(int minus, int plus, ICollection<Point> pointPlus, ICollection<Point> pointMinus) : this(minus, plus)
        {
            PositivePoints = pointPlus;
            NegativePoints = pointMinus;
        }
    }
}
