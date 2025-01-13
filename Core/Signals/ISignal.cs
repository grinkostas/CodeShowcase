namespace Core.Signals.Api
{
    public interface ISignal
    {
        SignalHub hub { get; set; }
        bool dispatchToParent { get; set; }
        string hash { get; }
    }
}