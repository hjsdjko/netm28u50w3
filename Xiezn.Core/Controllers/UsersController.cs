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
    /// 后台管理员相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly UsersService _bll;

        /// <summary>
        /// 构造函数
        /// </summary>
        public UsersController()
        {
            _bll = new UsersService();
        }

        /// <summary>
        /// 用户登录接口
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Login(string username, string password)
        {
            try
            {
                var userInfo = _bll.Login(username, password);
                if (userInfo == null)
                {
                    return Json(new { Code = -1, Msg = "账号或密码不正确!" });
                }

                TokenModel tokenModel = new TokenModel()
                {
                    Uid = userInfo.Id,
                    Uname = userInfo.Username,
                    Role = "Admin",
                    Project = "Xiezn.Core",
                    TokenType = "Web",
                    UNickname = "",
                    Tablename = ""
                };
                return Json(new { Code = 0, Token = JwtHelper.IssueJWT(tokenModel) });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 用户登出接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Logout()
        {
            return Json(new { Code = 0, Msg = "退出成功！" });
        }

        /// <summary>
        /// 注册接口
        /// </summary>
        /// <param name="entity">注册实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Register([FromBody] UsersDbModel entity)
        {
            try
            {
                if (_bll.BaseInsert(entity) > 0)
                {
                    return Json(new { Code = 0, Msg = "注册成功！" });
                }

                return Json(new { Code = -1, Msg = "注册失败！" });
            }
            catch (Exception ex)
            {
                return Json(new { Code = 500, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 获取session的接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public JsonResult Session()
        {
            try
            {
                TokenModel tm = CacheHelper.TokenModel;

                return Json(new { Code = 0, Data = _bll.BaseGetById(tm.Uid) });
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
        [Authorize(Roles = "Admin")]
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
        /// 保存接口
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Save([FromBody] UsersDbModel entity)
        {
            try
            {
                if (_bll.BaseInsert(entity) > 0)
                {
                    return Json(new { Code = 0, Msg = "添加成功！" });
                }

                return Json(new { Code = -1, Msg = "添加失败！" });
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
        [Authorize(Roles = "Admin")]
        public JsonResult Update([FromBody] UsersDbModel entity)
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
        /// 删除接口
        /// </summary>
        /// <param name="ids">主键int[]</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Delete([FromBody] dynamic[] ids)
        {
            try
            {
                if (_bll.BaseDels(ids))
                {
                    return Json(new { Code = 0, Msg = "删除成功！" });
                }

                return Json(new { Code = -1, Msg = "删除失败！" });
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
        [Authorize(Roles = "Admin")]
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
    }
}
