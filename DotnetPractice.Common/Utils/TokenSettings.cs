using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.Common.Utils
{
    public class TokenSettings
    {
        public string Site { get; set; }

        public string Secret { get; set; }

        public string ClientId { get; set; }

        public string ExpireTime { get; set; }

        public string Audience { get; set; }

        public string SendGridUser { get; set; }

        public string SendGridKey { get; set; }
    }
}
