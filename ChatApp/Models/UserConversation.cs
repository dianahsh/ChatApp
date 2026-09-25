using ChatApp.Data;

namespace ChatApp.Models
{
    public class UserConversation
    {
        public int Id { get; set; }

        public String MemberId { get; set; } = null!;
        public ApplicationUser Member { get; set; } = null!;


        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; } = null!;
    }
}
