namespace ChatApp.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsGroup { get; set; }
        public ICollection<Message> messages { get; set; } = [];
    }
}
