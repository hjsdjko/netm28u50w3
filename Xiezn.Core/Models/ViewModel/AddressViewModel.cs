using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xiezn.Core.Models.ViewModel
{
    public class AddressViewModel
    {
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; set; }
    }
}
