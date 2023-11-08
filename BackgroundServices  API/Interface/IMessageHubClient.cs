namespace BackgroundServices.Interface
{
    public interface IMessageHubClient
    {
        Task SendOffersToUser(List<string> message);
    }
}
