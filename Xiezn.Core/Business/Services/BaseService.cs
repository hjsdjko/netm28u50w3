using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xiezn.Core.Business.DB;
using Xiezn.Core.Models;

namespace Xiezn.Core.Business.Services
{
    public class BaseService<T> : DbContext<T> where T : class, new()
    {
        public PageModel<T> BaseGetPageList(int page, int limit, string sort, string order)
        {
            PageModel pageModel = new PageModel() { PageIndex = page, PageSize = limit };

            int totalNumber = 0;
            int totalPage = 0;

            string dbColumnName = Db.EntityMaintenance.GetDbColumnName<T>(sort);
            order = order.ToLower() == "asc" ? "ASC" : "DESC";

            List<IConditionalModel> conModels = new List<IConditionalModel>();
            conModels.Add(new ConditionalModel() { FieldName = "name", ConditionalType = ConditionalType.NoEqual, FieldValue ="APIKey" });
            conModels.Add(new ConditionalModel() { FieldName = "name", ConditionalType = ConditionalType.NoEqual, FieldValue ="SecretKey" });
            List<T> ts = Db.Queryable<T>().Where(conModels).OrderBy(dbColumnName + " " + order).ToPageList(page, limit, ref totalNumber, ref totalPage);

            PageModel<T> t = new PageModel<T>()
            {
                Code = ResponseCodeEnum.Success,
                Data = new Page<T>()
                {
                    Total = totalNumber,
                    PageSize = limit,
                    TotalPage = totalPage,
                    CurrPage = page,
                    List = ts
                }
            };

            return t;
        }

        public int BaseInsert(T entity)
        {
            //return CurrentDb.InsertReturnIdentity(entity);
            return Db.Insertable(entity).IgnoreColumns(true).ExecuteCommand();
        }

        public bool BaseUpdate(T entity)
        {
            return Db.Updateable(entity).IgnoreColumns(ignoreAllNullColumns: true).ExecuteCommand() > 0 ? true : false;
        }

        public bool BaseDels(dynamic[] ids)
        {
            return CurrentDb.DeleteByIds(ids);
        }

        public T BaseGetById(long id)
        {
            return CurrentDb.GetById(id);
        }

        public T BaseGetSingle(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.GetSingle(whereExpression);
        }

        public List<T> BaseGetList(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.GetList(whereExpression);
        }
        public int BaseGetCount(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.Count(whereExpression);
        }
    }
}
