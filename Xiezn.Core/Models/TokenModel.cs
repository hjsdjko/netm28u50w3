using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xiezn.Core.Models
{
    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long Uid { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Uname { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string UNickname { get; set; }
        /// <summary>
        /// 身份
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Project { get; set; }
        /// <summary>
        /// 令牌类型
        /// </summary>
        public string TokenType { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string Tablename { get; set; }
    }
}
