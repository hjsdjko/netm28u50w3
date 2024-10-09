using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xiezn.Core.Business.Services;
using Xiezn.Core.Common.Helpers;
using Xiezn.Core.Models;
using Xiezn.Core.Models.DbModel;

namespace Xiezn.Core.Controllers
{
    /// <summary>
    /// 系统配置相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    public class ConfigController : Controller
    {
        private readonly ConfigService _bll;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigController()
        {
            _bll = new ConfigService();
        }

        /// <summary>
        /// 分页接口
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页记录的长度</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">升序（默认asc）</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Client")]
        public JsonResult Page(int page = 1, int limit = 10, string sort = "id", string order = "asc")
        {
            try
            {
                return Json(_bll.BaseGetPageList(page, limit, sort, order));
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 分页接口
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页记录的长度</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">升序（默认asc）</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult List(int page = 1, int limit = 10, string sort = "id", string order = "asc")
        {
            try
            {
                return Json(_bll.BaseGetPageList(page, limit, sort, order));
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 更新接口
        /// </summary>
        /// <param name="entity">更新实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Client")]
        public JsonResult Update([FromBody] ConfigDbModel entity)
        {
            try
            {
                if (_bll.BaseUpdate(entity))
                {
                    return Json(new { Code = 0, Msg = "编辑成功！" });
                }

                return Json(new { Code = -1, Msg = "编辑失败！" });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }

        }

        /// <summary>
        /// 配置获取接口
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Client")]
        public JsonResult Info(string name)
        {
            try
            {
                return Json(new { Code = 0, Value = _bll.GetValueByName(name) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 详情接口
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Client")]
        public JsonResult Info(int id)
        {
            try
            {
                return Json(new { Code = 0, Data = _bll.BaseGetById(id) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 配置获取接口
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Client")]
        public JsonResult Detail(string name)
        {
            try
            {
                return Json(new { Code = 0, Value = _bll.GetValueByName(name) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }
    }
}
