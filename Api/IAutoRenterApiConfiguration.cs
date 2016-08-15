namespace Api
{
    public interface IAutoRenterApiConfiguration
    {
        int MaximumFileSizeInKb { get; }

        string AcceptedFileTypes { get; }

        bool IsAvailable { get; }
    }
}
