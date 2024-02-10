using System.Collections.Generic;
using System.Threading.Tasks;
using DesktopAppUI.Library.Models;

namespace DesktopAppUI.Library.Api
{
    public interface IUserApi
    {
        Task<List<ApplicationUserModel>> GetAll();
        Task<Dictionary<string, string>> GetAllRoles();
        Task AddRoleToUser(string userId, string roleName);
        Task RemoveRoleFromUser(string userId, string roleName);
    }
}