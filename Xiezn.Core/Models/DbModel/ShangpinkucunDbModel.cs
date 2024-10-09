using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace Xiezn.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 商品库存
    /// </summary>
    [SugarTable("shangpinkucun")]
	public class ShangpinkucunDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }

		/// <summary>
		/// Desc: 商品编号
		/// </summary>
		[SugarColumn(ColumnName = "shangpinbianhao")]
		public string Shangpinbianhao { get; set; }

		/// <summary>
		/// Desc: 商品名称
		/// </summary>
		[SugarColumn(ColumnName = "shangpinmingcheng")]
		public string Shangpinmingcheng { get; set; }

		/// <summary>
		/// Desc: 图片
		/// </summary>
		[SugarColumn(ColumnName = "tupian")]
		public string Tupian { get; set; }

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
		/// Desc: 成本价
		/// </summary>
        [SugarColumn(ColumnName = "chengbenjia")]
		public double? Chengbenjia { get; set; } = 0;

		/// <summary>
		/// Desc: 售价
		/// </summary>
        [SugarColumn(ColumnName = "shoujia")]
		public double? Shoujia { get; set; } = 0;

		/// <summary>
		/// Desc: 利润
		/// </summary>
        [SugarColumn(ColumnName = "lirun")]
		public double? Lirun { get; set; } = 0;

		/// <summary>
		/// Desc: 供应商
		/// </summary>
		[SugarColumn(ColumnName = "gongyingshang")]
		public string Gongyingshang { get; set; }

		/// <summary>
		/// Desc: 日期
		/// </summary>
        [SugarColumn(ColumnName = "riqi")]
		[JsonConverter(typeof(Common.Helpers.DateFormatConverter), "yyyy-MM-dd")]
		public DateTime Riqi { get; set; }

		/// <summary>
		/// Desc: 最近点击时间
		/// </summary>
        [SugarColumn(ColumnName = "clicktime")]
		public DateTime? Clicktime { get; set; }

		/// <summary>
		/// Desc: 点击次数
		/// </summary>
        [SugarColumn(ColumnName = "clicknum")]
		public int? Clicknum { get; set; } = 0;

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
