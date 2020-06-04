using System;

namespace Inha.Commons.Messages
{
    public class Done : InProgress
    {
        public Done() : base()
        {

        }
        public string Action { get; set; }
        public string UserUpdated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CHEId { get; set; }
    }
}
