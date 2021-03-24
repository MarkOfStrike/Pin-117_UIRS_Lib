using Pin_117_UIRS_Lib_Core.Structs.Interfaces;

namespace Pin_117_UIRS_Lib_Core.Structs
{
    public struct Value : IValue
    {
        public double MinValue { get; }
        public int Index { get; }

        public Value(double minValue, int index)
        {
            MinValue = minValue;
            Index = index;
        }
    }
}