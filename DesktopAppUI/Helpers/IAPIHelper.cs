using System.Threading.Tasks;
using DesktopAppUI.Models;

namespace DesktopAppUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}