namespace ChatApplicationSignalR.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string? Sender { get; set; }
        public string? Receiver { get; set; }
        public string? Content { get; set; }
        public DateTime TimeStamp { get; set; }

        public int ConversationId { get; set; }
        public Conversation? Conversation { get; set; }
    }
}
