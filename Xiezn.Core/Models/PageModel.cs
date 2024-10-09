using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xiezn.Core.Models
{
    /// <summary>
    /// 通用分页信息类
    /// </summary>
    public class PageModel<T>
    {
        /// <summary>
        /// 返回编码
        /// </summary>
        public ResponseCodeEnum Code { get; set; }

        /// <summary>
        /// 分页数据
        /// </summary>
        public Page<T> Data { get; set; }
    }

    public class Page<T>
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 当前页标
        /// </summary>
        public int CurrPage { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public List<T> List { get; set; }
    }
}
