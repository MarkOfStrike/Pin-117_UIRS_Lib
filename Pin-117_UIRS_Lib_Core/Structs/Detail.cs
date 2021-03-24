using System.Collections.Generic;
using System.Linq;

using Pin_117_UIRS_Lib_Core.Enums;
using Pin_117_UIRS_Lib_Core.Structs.Interfaces;

namespace Pin_117_UIRS_Lib_Core.Structs
{
    public struct Detail : IDetail, IDetailError
    {
        private List<double[]> _standartsLearn;
        private string _nameObject;

        private int _countError;

        public List<double[]> Standarts => _standartsLearn.ToList();
        public string Name => _nameObject;
        public int CountError => _countError;

        public Detail(List<double[]> standarts, string nameObject, TypeDetail type)
        {
            var del = (int)(standarts.Count * 0.8);

            var setStandart = new List<double[]>();

            switch (type)
            {
                case TypeDetail.All:
                    setStandart = standarts;
                    break;
                case TypeDetail.Train:
                    setStandart = standarts.GetRange(0, del);
                    break;
                case TypeDetail.Test:
                    setStandart = standarts.GetRange(del - 1, standarts.Count - del);
                    break;
                default:
                    break;
            }

            _standartsLearn = setStandart;
            _nameObject = nameObject.Substring(0, nameObject.LastIndexOf('.'));
            _countError = 0;
        }

        public void SetError(int errorCount)
        {
            _countError = errorCount;
        }
    }
}
