namespace Api
{
    public interface IAutoRenterApiConfiguration
    {
        string TestEmail { get; }

        int MaximumFileSizeInKb { get; }

        string AcceptedFileTypes { get; }
    }
}
