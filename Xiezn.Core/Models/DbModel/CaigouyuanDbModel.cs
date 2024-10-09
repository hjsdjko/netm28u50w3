using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace Xiezn.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 采购员
    /// </summary>
    [SugarTable("caigouyuan")]
	public class CaigouyuanDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }

		/// <summary>
		/// Desc: 采购工号
		/// </summary>
		[SugarColumn(ColumnName = "caigougonghao")]
		public string Caigougonghao { get; set; }

		/// <summary>
		/// Desc: 采购姓名
		/// </summary>
		[SugarColumn(ColumnName = "caigouxingming")]
		public string Caigouxingming { get; set; }

		/// <summary>
		/// Desc: 密码
		/// </summary>
		[SugarColumn(ColumnName = "mima")]
		public string Mima { get; set; }

		/// <summary>
		/// Desc: 性别
		/// </summary>
		[SugarColumn(ColumnName = "xingbie")]
		public string Xingbie { get; set; }

		/// <summary>
		/// Desc: 职称
		/// </summary>
		[SugarColumn(ColumnName = "zhicheng")]
		public string Zhicheng { get; set; }

		/// <summary>
		/// Desc: 联系电话
		/// </summary>
		[SugarColumn(ColumnName = "lianxidianhua")]
		public string Lianxidianhua { get; set; }

		/// <summary>
		/// Desc: 头像
		/// </summary>
		[SugarColumn(ColumnName = "touxiang")]
		public string Touxiang { get; set; }

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
