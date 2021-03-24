namespace Pin_117_UIRS_Lib_Core.Structs.Interfaces
{
    public interface IDetailError : IDetail
    {
        int CountError { get; }
        void SetError(int errorCount);
    }
}
