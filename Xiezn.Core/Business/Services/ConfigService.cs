using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xiezn.Core.Models.DbModel;

namespace Xiezn.Core.Business.Services
{
    public class ConfigService : BaseService<ConfigDbModel>
    {
        public string GetValueByName(string name)
        {
            return Db.Ado.GetString("SELECT value FROM config WHERE name = @name", new SugarParameter("@name", name));
        }

        public int UpdateByName(string name, string value)
        {
            return Db.Ado.ExecuteCommand("UPDATE config SET value = @value WHERE name = @name", new SugarParameter[] {
                new SugarParameter("@name", name),
                new SugarParameter("@value", value)
            });
        }
    }
}
