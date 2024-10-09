using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;
using Newtonsoft.Json;

namespace Xiezn.Core.Models.DbModel
{
    /// <summary>
    ///	Desc: 入库信息
    /// </summary>
    [SugarTable("rukuxinxi")]
	public class RukuxinxiDbModel
	{           
		/// <summary>
		/// Desc: 主键Id
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
		public long Id { get; set; }

		/// <summary>
		/// Desc: 进货单号
		/// </summary>
		[SugarColumn(ColumnName = "jinhuodanhao")]
		public string Jinhuodanhao { get; set; }

		/// <summary>
		/// Desc: 商品名称
		/// </summary>
		[SugarColumn(ColumnName = "shangpinmingcheng")]
		public string Shangpinmingcheng { get; set; }

		/// <summary>
		/// Desc: 商品编号
		/// </summary>
		[SugarColumn(ColumnName = "shangpinbianhao")]
		public string Shangpinbianhao { get; set; }

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
		/// Desc: 入库时间
		/// </summary>
        [SugarColumn(ColumnName = "rukushijian")]
		public DateTime? Rukushijian { get; set; }

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
		/// Desc: 供应商
		/// </summary>
		[SugarColumn(ColumnName = "gongyingshang")]
		public string Gongyingshang { get; set; }

		/// <summary>
		/// Desc: 添加时间
		/// </summary>
		[SugarColumn(ColumnName = "addtime")]
		public DateTime? Addtime { get; set; } = DateTime.Now;

	}
}
