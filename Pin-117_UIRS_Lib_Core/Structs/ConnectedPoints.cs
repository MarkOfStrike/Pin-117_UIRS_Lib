using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin_Library_UIRS_Core.Structs
{
    public struct ConnectedPoints
    {
        /// <summary>
        /// Количество 4-х связных опорных точек
        /// </summary>
        public int K;

        /// <summary>
        /// Количество D-связных опорных точек
        /// </summary>
        public int T;

        /// <summary>
        /// Список 4-х связных опорных точек
        /// </summary>
        public ICollection<Point> pointsK;

        /// <summary>
        /// Список D-связных опорных точек
        /// </summary>
        public ICollection<Point> pointsT;
    }
}
