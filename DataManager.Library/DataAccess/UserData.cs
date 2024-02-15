using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager.Library.Internal.DataAccess;
using DataManager.Library.Models;
using Microsoft.Extensions.Configuration;

namespace DataManager.Library.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public UserData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public List<UserModel> GetUserById(string Id)
        {
            var p = new { Id = Id };

            var output = _sqlDataAccess.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "AppData");

            return output;
        }
    }
}
