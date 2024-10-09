using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace Xiezn.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 销售订单
    /// </summary>
    [SugarTable("xiaoshoudingdan")]
	public class XiaoshoudingdanDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }

		/// <summary>
		/// Desc: 销售单号
		/// </summary>
		[SugarColumn(ColumnName = "xiaoshoudanhao")]
		public string Xiaoshoudanhao { get; set; }

		/// <summary>
		/// Desc: 商品名称
		/// </summary>
		[SugarColumn(ColumnName = "shangpinmingcheng")]
		public string Shangpinmingcheng { get; set; }

		/// <summary>
		/// Desc: 商品类别
		/// </summary>
		[SugarColumn(ColumnName = "shangpinleibie")]
		public string Shangpinleibie { get; set; }

		/// <summary>
		/// Desc: 售价
		/// </summary>
        [SugarColumn(ColumnName = "shoujia")]
		public double? Shoujia { get; set; } = 0;

		/// <summary>
		/// Desc: 数量
		/// </summary>
        [SugarColumn(ColumnName = "shuliang")]
		public int? Shuliang { get; set; } = 0;

		/// <summary>
		/// Desc: 总金额
		/// </summary>
        [SugarColumn(ColumnName = "zongjine")]
		public double? Zongjine { get; set; } = 0;

		/// <summary>
		/// Desc: 销售时间
		/// </summary>
        [SugarColumn(ColumnName = "xiaoshoushijian")]
		public DateTime? Xiaoshoushijian { get; set; }

		/// <summary>
		/// Desc: 员工工号
		/// </summary>
		[SugarColumn(ColumnName = "yuangonggonghao")]
		public string Yuangonggonghao { get; set; }

		/// <summary>
		/// Desc: 员工姓名
		/// </summary>
		[SugarColumn(ColumnName = "yuangongxingming")]
		public string Yuangongxingming { get; set; }

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
