using System.Collections.Generic;

namespace Pin_117_UIRS_Lib_Core.Structs.Interfaces
{
    public interface IDetail
    {
        IList<double[]> Standarts { get; }
        string Name { get; }
    }
}
