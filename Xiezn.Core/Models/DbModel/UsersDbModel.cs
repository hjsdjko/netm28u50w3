using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xiezn.Core.Models.DbModel
{
    /// <summary>
    /// 后台管理员用户表
    /// </summary>
    [SugarTable("users")]
    public class UsersDbModel
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(ColumnName = "username")]
        public string Username { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [SugarColumn(ColumnName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        [SugarColumn(ColumnName = "role")]
        public string Role { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [SugarColumn(ColumnName = "addtime")]
        public DateTime? Addtime { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [SugarColumn(ColumnName = "image")]
        public string Image { get; set; }
    }
}
