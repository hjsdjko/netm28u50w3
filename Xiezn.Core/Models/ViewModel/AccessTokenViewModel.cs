using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xiezn.Core.Models.ViewModel
{
    public class AccessTokenViewModel
    {
        public string refresh_token { get; set; }

        public int expires_in { get; set; }

        public string scope { get; set; }

        public string session_key { get; set; }

        public string access_token { get; set; }

        public string session_secret { get; set; }
    }
}
