namespace Formula1Import.Contracts.ExternalServices;

public interface ISlackClient
{
    void SendMessage(string message);
}
