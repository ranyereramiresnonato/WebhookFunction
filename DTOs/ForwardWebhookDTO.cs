
namespace ForwardWebhook.Models
{
    public class ForwardWebhookDTO
    {
        public string DestinationIdentifier { get; set; }
        public object? Payload { get; set; }
    }
}
