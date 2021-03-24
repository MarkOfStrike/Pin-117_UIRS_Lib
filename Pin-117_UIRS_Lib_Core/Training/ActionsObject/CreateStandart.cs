using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Pin_117_UIRS_Lib_Core.Structs;
using Pin_117_UIRS_Lib_Core.Structs.Interfaces;


namespace Pin_117_UIRS_Lib_Core.Training.ActionsObject
{
    public static class CreateStandart
    {
        /// <summary>
        /// Построение эталонов для набора объектов
        /// </summary>
        /// <param name="details">Список объектов и их признаков</param>
        /// <param name="resultPath">Путь для сохранения результатов</param>
        public static void Build(IList<IDetail> details, string resultPath)
        {
            var random = new Random();

            for (int i = 0; i < details.Count; i++)
            {
                var currentKMas = details[i].Standarts;

                var index = random.Next(currentKMas.Count);
                var currentStandart = currentKMas[index];
                currentKMas.RemoveAt(index);

                var resultMas = new List<Value>();
                var resultK = new List<double[]>();

                    Console.Write($"Создание эталонов для объекта {details[i].Name}: ");

                    var count = (double)currentKMas.Count;
                    var countMas = currentKMas.Count;


                    while (currentKMas.Count > 0)
                    {
                        for (int j = 0; j < details.Count; j++)
                        {
                            var value = MinValueDetail.GetMinValue(i == j ? currentKMas : details[j].Standarts, currentStandart);
                            resultMas.Add(value);
                        }

                        var tmp = resultMas.Select(v => v.MinValue).ToList();

                        var indexMin = tmp.IndexOf(tmp.Min());
                        index = random.Next(currentKMas.Count);

                        if (indexMin != i)
                        {
                            resultK.Add(currentStandart);
                            currentStandart = currentKMas[index];
                            currentKMas.RemoveAt(index);
                        }
                        else
                        {
                            currentKMas.RemoveAt(resultMas[indexMin].Index);
                        }

                        resultMas.Clear();

                    }

                Console.WriteLine("Выполнено!");

                WriteResult(resultK, resultPath, details[i].Name);
                resultK.Clear();
            }
        }


        /// <summary>
        /// Запись результирующего списка эталонов
        /// </summary>
        /// <param name="results">Список эталонов</param>
        /// <param name="path">Путь к файлу</param>
        /// <param name="name">Имя файла</param>
        private static void WriteResult(List<double[]> results, string path, string name)
        {
            using (var str = new StreamWriter($"{path}/{name}_standarts.csv", false, Encoding.UTF8))
            {
                foreach (var result in results)
                {
                    str.WriteLine(string.Join(";", result));
                    str.Flush();
                }

                str.Close();
            }
        }

    }
}