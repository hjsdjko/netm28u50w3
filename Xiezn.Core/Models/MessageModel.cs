using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xiezn.Core.Models
{
    /// <summary>
    /// 通用返回信息类
    /// </summary>
    public class MessageModel<T>
    {
        /// <summary>
        /// 返回编码
        /// </summary>
        public ResponseCodeEnum Code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回数据集合
        /// </summary>
        public T Data { get; set; }
    }
}