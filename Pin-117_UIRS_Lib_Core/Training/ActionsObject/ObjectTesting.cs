using System;
using System.Collections.Generic;
using System.Linq;

using Pin_117_UIRS_Lib_Core.Structs.Interfaces;


namespace Pin_117_UIRS_Lib_Core.Training.ActionsObject
{
    public static class ObjectTesting
    {

        /// <summary>
        /// Тестирование распознания объекта
        /// </summary>
        /// <param name="details">Объекты для распознания</param>
        /// <param name="standarts">Набор эталонов</param>
        public static void Testing(IList<IDetailError> details, IList<IDetail> standarts)
        {
            for (int i = 0; i < details.Count; i++)
            {
                var dt = details[i].Standarts;

                var results = new List<double>();
                var countError = 0;

                    Console.Write($"Тестирование объекта {details[i].Name}: ");

                    var count = dt.Count;

                    for (int j = 0; j < count; j++)
                    {
                        foreach (var standart in standarts)
                        {
                            var value = (IValue)MinValueDetail.GetMinValue(standart.Standarts, dt[j]);
                            results.Add(value.MinValue);
                        }

                        var index = results.IndexOf(results.Min());

                        if (index != i)
                        {
                            countError++;
                        }

                        results.Clear();
                    }


                Console.WriteLine($"кол-во ошибок {countError}/{dt.Count} | точность вычислений {(double)(dt.Count - countError) / dt.Count}");

                details[i].SetError(countError);

            }

        }

    }
}
