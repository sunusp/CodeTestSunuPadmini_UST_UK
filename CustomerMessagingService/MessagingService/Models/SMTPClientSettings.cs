namespace MessagingService.Models
{
    public class SMTPClientSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string NetworkUserName { get; set; }
        public string NetworkPassword { get; set; }
        public string FromEmail { get; set; }
    }
}
