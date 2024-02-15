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
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public ProductData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public List<ProductModel> GetProducts()
        {
            var output = _sqlDataAccess.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "AppData");

            return output;
        }

        public ProductModel GetProductById(int productId)
        {
            var output = _sqlDataAccess.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = productId }, "AppData").FirstOrDefault();

            return output;
        }
    }
}
