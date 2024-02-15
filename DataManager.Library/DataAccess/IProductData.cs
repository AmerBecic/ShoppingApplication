using System.Collections.Generic;
using DataManager.Library.Models;

namespace DataManager.Library.DataAccess
{
    public interface IProductData
    {
        ProductModel GetProductById(int productId);
        List<ProductModel> GetProducts();
    }
}