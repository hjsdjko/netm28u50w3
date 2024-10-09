using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Xiezn.Core.Models
{
    public enum ResponseCodeEnum
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        [Description("请求成功")]
        Success = 0,
        /// <summary>
        /// 无权限
        /// </summary>
        [Description("无权限")]
        NoPermissions = 401,
        /// <summary>
        /// 找不到指定资源
        /// </summary>
        [Description("找不到指定资源")]
        NoFound = 404,
        /// <summary>
        /// 找不到指定资源
        /// </summary>
        [Description("服务器错误")]
        ServerError = 500
    }
}