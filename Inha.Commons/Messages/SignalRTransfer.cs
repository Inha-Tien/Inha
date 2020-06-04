namespace Inha.Commons.Messages
{
    public class SignalRTransfer<T> : KafkaTransfer<T>
    {
        public string Topic { get; set; }
    }
}
