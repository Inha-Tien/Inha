using Inha.Commons.Types;
using System;
using System.Collections.Generic;

namespace Inha.Commons.Messages
{
    public class KafkaTransfer<T> : ICommand
    {
        public string Guid { get; set; }
        public string UtcDate { get; set; }
        public string Sender { get; set; }
        public string Object { get; set; }
        public T Data { get; set; }
        public IEnumerable<KeyVal> ParameterPass { get; set; }
        public HATEOAS Links { get; set; }

        public KafkaTransfer()
        {
            //this.Guid = System.Guid.NewGuid().ToString();
            //this.UtcDate = DateTime.UtcNow.ToString("dd/MM/yyyy hh:mm:ss.FFFZ");
        }
    }
}
