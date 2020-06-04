using System.Collections.Generic;

namespace Inha.Commons.RestEase
{
    public class RestEaseOptions
    {
        public string LoadBalancer { get; set; }
        public IEnumerable<Service> Services { get; set; }

        public class Service
        {
            public string Name { get; set; }
            public string Scheme { get; set; }
            public string Host { get; set; }
            public string Port { get; set; }
        }
    }
}