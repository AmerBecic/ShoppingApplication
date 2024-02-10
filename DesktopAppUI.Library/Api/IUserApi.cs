using System.Collections.Generic;
using System.Threading.Tasks;
using DesktopAppUI.Library.Models;

namespace DesktopAppUI.Library.Api
{
    public interface IUserApi
    {
        Task<List<ApplicationUserModel>> GetAll();
    }
}