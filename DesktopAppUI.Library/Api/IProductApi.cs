using System.Collections.Generic;
using System.Threading.Tasks;
using DesktopAppUI.Library.Models;

namespace DesktopAppUI.Library.Api
{
    public interface IProductApi
    {
        Task<List<ProductModel>> GetAll();
    }
}