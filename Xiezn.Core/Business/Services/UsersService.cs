using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xiezn.Core.Models.DbModel;

namespace Xiezn.Core.Business.Services
{
    public class UsersService : BaseService<UsersDbModel>
    {
        public dynamic Login(string username, string password)
        {
            return CurrentDb.GetSingle(it => it.Username == username && it.Password == password);
        }
    }
}
