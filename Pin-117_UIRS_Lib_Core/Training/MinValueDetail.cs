using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Pin_117_UIRS_Lib_Core.Structs;

namespace Pin_117_UIRS_Lib_Core.Training
{
    public static class MinValueDetail
    {

        /// <summary>
        /// Нахождение наименьшего расстояния между векторами внутри объекта
        /// </summary>
        /// <param name="source">Массив векторов</param>
        /// <param name="value">Вектор</param>
        /// <returns>Расстояние</returns>
        public static Value GetMinValue(IEnumerable<double[]> source, IReadOnlyList<double> value)
        {
            var tmpList = new List<double>();

            foreach (var item in source)
            {
                var tmp = 0D;

                for (int i = 0; i < item.Length; i++)
                {
                    tmp += Math.Pow(item[i] - value[i], 2);
                }

                tmpList.Add(Math.Sqrt(tmp));
            }

            var min = tmpList.Min();
            var index = tmpList.IndexOf(min);

            return new Value(min, index);
        }

    }
}
