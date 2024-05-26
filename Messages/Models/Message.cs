namespace Messages.Models
{
    public class Message
    {
        public string Text { get; set; }
        public int Id { get; set; }
        public string Author { get; set; }  
        public DateTime Created { get; set; }
    }
}
