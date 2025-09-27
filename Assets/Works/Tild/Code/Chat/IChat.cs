namespace Code.Chat
{
    public struct Message
    {
        public string message;
        public float delay;
    }
    public interface IChat
    {
       public ChatType ChatType { get; }
       public Message Message { get; }
    }
}