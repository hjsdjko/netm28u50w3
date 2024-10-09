using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xiezn.Core.Common.Helpers;
using Xiezn.Core.Models;
using Xiezn.Core.Models.DbModel;


namespace Xiezn.Core.Business.Services
{
    public class ShangpinkucunService : BaseService<ShangpinkucunDbModel>
    {
        private readonly long _uid;
        private readonly string _role;
        private readonly string _tablename;

        public ShangpinkucunService()
        {
            try
            {
                if (CacheHelper.TokenModel != null)
                {
                    _uid = CacheHelper.TokenModel.Uid;
                    _role = CacheHelper.TokenModel.Role;
                    _tablename = CacheHelper.TokenModel.Tablename;
                }
            }
            catch
            {
                _uid = 0;
                _role = "游客";
            }
        }



        public int IntelRecom(long id)
        {
            ShangpinkucunDbModel updateObj = new ShangpinkucunDbModel();
            //return Db.Updateable(updateObj).UpdateColumns(it => new { it.Clicktime }).ReSetValue(it => it.Clicktime == (it.Clicktime + 1)).Where(it => it.Id == id).ExecuteCommand();
            return Db.Updateable(updateObj).UpdateColumns(it => new { it.Clicktime }).ReSetValue(it => it.Clicktime == DateTime.Now).Where(it => it.Id == id).ExecuteCommand();
        }

		public int BrowseClick(long id)
        {
            ShangpinkucunDbModel updateObj = new ShangpinkucunDbModel();
            return Db.Updateable(updateObj).UpdateColumns(it => new { it.Clicknum }).ReSetValue(it => it.Clicknum == (it.Clicknum + 1)).Where(it => it.Id == id).ExecuteCommand();
        }


        public PageModel<ShangpinkucunDbModel> GetPageList(int page, int limit, string sort, string order, List<IConditionalModel> conModels)
        {
            PageModel pageModel = new PageModel() { PageIndex = page, PageSize = limit };

            int totalNumber = 0;
            int totalPage = 0;
            string[] sortFields = sort.Split(',');
            string[] orderFields = order.Split(',');
            string mysort = "";
            for (int i = 0; i < sortFields.Length; i++)
            {
                if (i == sortFields.Length - 1)
                {
                    mysort += sortFields[i] + " " + orderFields[i];
                }
                else
                {
                    mysort += sortFields[i] + " " + orderFields[i] + ",";

                }

            }
            List<ShangpinkucunDbModel> ts = Db.Queryable<ShangpinkucunDbModel>().Where(conModels).OrderBy(mysort).ToPageList(page, limit, ref totalNumber, ref totalPage);


            PageModel<ShangpinkucunDbModel> t = new PageModel<ShangpinkucunDbModel>()
            {
                Code = ResponseCodeEnum.Success,
                Data = new Page<ShangpinkucunDbModel>()
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








    }
}
