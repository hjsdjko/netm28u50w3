using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace Xiezn.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 供应商
    /// </summary>
    [SugarTable("gongyingshang")]
	public class GongyingshangDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }

		/// <summary>
		/// Desc: 供应商
		/// </summary>
		[SugarColumn(ColumnName = "gongyingshang")]
		public string Gongyingshang { get; set; }

		/// <summary>
		/// Desc: 地址
		/// </summary>
		[SugarColumn(ColumnName = "dizhi")]
		public string Dizhi { get; set; }

		/// <summary>
		/// Desc: 联系电话
		/// </summary>
		[SugarColumn(ColumnName = "lianxidianhua")]
		public string Lianxidianhua { get; set; }

		/// <summary>
		/// Desc: 负责人
		/// </summary>
		[SugarColumn(ColumnName = "fuzeren")]
		public string Fuzeren { get; set; }

		/// <summary>
		/// Desc: 职务
		/// </summary>
		[SugarColumn(ColumnName = "zhiwu")]
		public string Zhiwu { get; set; }

		/// <summary>
		/// Desc: 营业执照号码
		/// </summary>
		[SugarColumn(ColumnName = "yingyezhizhaohaoma")]
		public string Yingyezhizhaohaoma { get; set; }

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
