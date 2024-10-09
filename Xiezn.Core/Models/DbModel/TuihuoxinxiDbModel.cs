using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace Xiezn.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 退货信息
    /// </summary>
    [SugarTable("tuihuoxinxi")]
	public class TuihuoxinxiDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }

		/// <summary>
		/// Desc: 退货单号
		/// </summary>
		[SugarColumn(ColumnName = "tuihuodanhao")]
		public string Tuihuodanhao { get; set; }

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
		/// Desc: 数量
		/// </summary>
        [SugarColumn(ColumnName = "shuliang")]
		public int? Shuliang { get; set; } = 0;

		/// <summary>
		/// Desc: 售价
		/// </summary>
        [SugarColumn(ColumnName = "shoujia")]
		public double? Shoujia { get; set; } = 0;

		/// <summary>
		/// Desc: 退货金额
		/// </summary>
        [SugarColumn(ColumnName = "tuihuojine")]
		public double? Tuihuojine { get; set; } = 0;

		/// <summary>
		/// Desc: 退货日期
		/// </summary>
        [SugarColumn(ColumnName = "tuihuoriqi")]
		public DateTime? Tuihuoriqi { get; set; }

		/// <summary>
		/// Desc: 员工工号
		/// </summary>
		[SugarColumn(ColumnName = "yuangonggonghao")]
		public string Yuangonggonghao { get; set; }

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
