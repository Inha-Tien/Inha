namespace Inha.Commons.Messages
{
    public class REQ : InProgress
    {
        public REQ() : base()
        {

        }
        public string Action { get; set; }
        public string Label { get; set; }

    }

}
