namespace ChatApplicationSignalR.Models
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public List<User> Users { get; set; } = new();
        public List<Message> Messages { get; set; } = new();
    }
}
