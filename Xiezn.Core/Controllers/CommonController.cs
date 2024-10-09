using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using Xiezn.Core.Business.Services;
using Xiezn.Core.Common.Helpers;
using Xiezn.Core.Models.ViewModel;

namespace Xiezn.Core.Controllers
{
    /// <summary>
    /// 公共接口
    /// </summary>
    [Route("[action]")]
    public class CommonController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _savePath;
        private readonly CommonService _bll;
        private readonly ConfigService _configBLL;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommonController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _savePath = _hostingEnvironment.WebRootPath + Path.DirectorySeparatorChar + ConfigHelper.GetConfig("SchemaName") + Path.DirectorySeparatorChar + "upload" + Path.DirectorySeparatorChar;
            _bll = new CommonService();
            _configBLL = new ConfigService();
        }

        /// <summary>
        /// 获取某表的某个字段列表接口
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        [HttpGet("{tableName}/{columnName}")]
        public JsonResult Option(string tableName, string columnName)
        {
            try
            {
                int level = Convert.ToInt32(HttpContext.Request.Query["level"]);
                string parent = HttpContext.Request.Query["parent"];
                string conditionColumn = HttpContext.Request.Query["conditionColumn"];
                string conditionValue = HttpContext.Request.Query["conditionValue"];
                if (!string.IsNullOrEmpty(conditionColumn) && !string.IsNullOrEmpty(conditionValue))
                {
                    return Json(new { Code = 0, Data = _bll.Common(tableName, columnName, "", 0, "option", 0, 0, " AND " + conditionColumn + " = '" + conditionValue + "' ") });
                }
                if (level == 0)
                {
                    return Json(new { Code = 0, Data = _bll.Common(tableName, columnName, parent, level) });
                }
                else
                {
                    return Json(new { Code = 0, Data = _bll.Common(tableName, columnName) });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 根据option字段值获取某表的单行记录接口
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="columnValue">列值</param>
        /// <returns></returns>
        [HttpGet("{tableName}/{columnName}")]
        public JsonResult Follow(string tableName, string columnName, string columnValue)
        {
            try
            {
                return Json(new { Code = 0, Data = _bll.Common(tableName, columnName, columnValue, 0, "follow") });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 根据主键id修改table表的sfsh状态接口
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="id">主键id</param>
        /// <param name="sfsh">当前审核状态（是/否）</param>
        /// <returns></returns>
        [HttpPost("{tableName}")]
        [Authorize(Roles = "Admin,Client")]
        public JsonResult Sh(string tableName, int id, string sfsh)
        {
            try
            {
                if (_bll.Common(tableName, id.ToString(), sfsh, 0, "sh") > 0)
                {
                    return Json(new { Code = 0, Msg = "更新成功！" });
                }

                return Json(new { Code = -1, Msg = "更新失败！" });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 获取需要提醒的记录数接口
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="type">类型（1表示数字比较提醒，2表示日期比较提醒）</param>
        /// <param name="remindStart">remindStart小于等于columnName满足条件提醒,当比较日期时，该值表示天数</param>
        /// <param name="remindEnd">columnName小于等于remindEnd 满足条件提醒,当比较日期时，该值表示天数</param>
        /// <returns></returns>
        [HttpGet("{tableName}/{columnName}/{type}")]
        public JsonResult Remind(string tableName, string columnName, int type, int remindStart, int remindEnd)
        {
            try
            {
                return Json(new { Code = 0, Count = _bll.Common(tableName, columnName, "", type, "remind", remindStart, remindEnd) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 计算规则接口
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        [HttpGet("{tableName}/{columnName}")]
        public JsonResult Cal(string tableName, string columnName)
        {
            try
            {
                return Json(new { Code = 0, Data = _bll.Common(tableName, columnName, "", 0, "cal") });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 人脸比较
        /// </summary>
        /// <param name="face1">图片1名称</param>
        /// <param name="face2">图片2名称</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult MatchFace(string face1, string face2)
        {
            try
            {
                BaiduAiHelper.clientId = _configBLL.GetValueByName("APIKey");
                BaiduAiHelper.clientSecret = _configBLL.GetValueByName("SecretKey");
                BaiduAiHelper.GetAccessToken();
                List<FaceMatchViewModel> matchInfo = new List<FaceMatchViewModel>
                {
                    new FaceMatchViewModel { image = FuncHelper.ImageToBase64(_savePath + face1) },
                    new FaceMatchViewModel { image = FuncHelper.ImageToBase64(_savePath + face2) }
                };
                string result = BaiduAiHelper.FaceMatch(JsonConvert.SerializeObject(matchInfo));
                dynamic resObj = JsonConvert.DeserializeObject(result);
                if (resObj.error_code == 0)
                {
                    return Json(new { Code = 0, Score = resObj.result.score, Msg = "匹配成功！" });
                }
                else
                {
                    return Json(new { Code = -1, Score = 0, Msg = "匹配失败！" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 定位接口（根据经纬度坐标获取到省市县(区)信息）
        /// </summary>
        /// <param name="lat">经度</param>
        /// <param name="lng">纬度</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Location(double lat, double lng)
        {
            try
            {
                AddressViewModel addressViewModel = BaiduAiHelper.GetAddress(_configBLL.GetValueByName("baidu_ditu_ak"), lng, lat);
                if (addressViewModel == null)
                {
                    return Json(new { Code = -1, Msg = "位置信息获取失败！" });
                }
                else
                {
                    return Json(new { Code = 0, Data = addressViewModel });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 类别统计接口
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        [HttpGet("{tableName}/{columnName}")]
        public JsonResult Group(string tableName, string columnName)
        {
            try
            {
                return Json(new { Code = 0, Data = _bll.Common(tableName, columnName, "", 0, "group") });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 按值统计接口
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="xColumnName">列名</param>
        /// <param name="yColumnName">列名</param>
        /// <returns></returns>
        [HttpGet("{tableName}/{xColumnName}/{yColumnName}")]
        public JsonResult Value(string tableName, string xColumnName, string yColumnName)
        {
            try
            {
                return Json(new { Code = 0, Data = _bll.Common(tableName, xColumnName, yColumnName, 0, "value") });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 按时间统计类型接口
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="xColumnName">列名</param>
        /// <param name="yColumnName">列名</param>
        /// <param name="timeStatType">类型</param>
        /// <returns></returns>
        [HttpGet("{tableName}/{xColumnName}/{yColumnName}/{timeStatType}")]
        public JsonResult Value(string tableName, string xColumnName, string yColumnName, string timeStatType)
        {
            try
            {
                return Json(new { Code = 0, Data = _bll.StatDate(tableName, xColumnName, yColumnName, timeStatType) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }



    }
}
