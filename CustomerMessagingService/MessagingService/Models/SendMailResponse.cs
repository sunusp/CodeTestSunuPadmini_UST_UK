namespace MessagingService.Models
{
    public class SendMailResponse
    {
        public bool Sent { get; set; }
        public Exception? Error { get; set; }
    }
}
