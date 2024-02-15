using System.Collections.Generic;
using DataManager.Library.Models;

namespace DataManager.Library.DataAccess
{
    public interface IUserData
    {
        List<UserModel> GetUserById(string Id);
    }
}