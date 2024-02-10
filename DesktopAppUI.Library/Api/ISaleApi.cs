using System.Threading.Tasks;
using DesktopAppUI.Library.Models;

namespace DesktopAppUI.Library.Api
{
    public interface ISaleApi
    {
        Task PostSale(SaleModel sale);
    }
}