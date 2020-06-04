using System;

namespace Inha.Commons.Messages
{
    public class DoneDelete : InProgress
    {
        public DoneDelete() : base()
        {

        }
        public string Action { get; set; }
        public string UserUpdated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
