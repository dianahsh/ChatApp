using ChatApp.Data;

namespace ChatApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime SendDate { get; set; } = DateTime.Now;
        public string? Text { get; set; }


        public string SenderId { get; set; } = null!;
        public ApplicationUser Sender { get; set; } = null!;

        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; } = null!;
    }
}
