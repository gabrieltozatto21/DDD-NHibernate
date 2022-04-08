using System.Collections.Generic;

namespace DDD.NHibernate.Libs.Core.Api.Logs
{
    public class HttpContextCache
    {
        public string TransactionId { get; set; }

        public string Path { get; set; }

        public string Method { get; set; }

        public string QueryString { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public string Body { get; set; }
    }
}
