using System.Threading.Tasks;
using DesktopAppUI.Library.Models;

namespace DesktopAppUI.Library.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserInfo(string token);
    }
}