using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager.Library.Internal.DataAccess;
using DataManager.Library.Models;

namespace DataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            //TODO make this SOLID, split into parts!!!
            //Start filling in the models we will save to the DB
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            ProductData products = new ProductData();
            decimal taxRate = ConfigHelper.GetTaxRate();

            foreach (var product in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                };

                //get info about this product
                var productInfo = products.GetProductById(detail.ProductId);

                if (productInfo == null)
                {
                    throw new Exception($"The product Id of {detail.ProductId} cloud not be found in the database.");
                }

                detail.PurchasePrice = productInfo.RetailPrice * detail.Quantity;

                if (productInfo.IsTaxable)
                {
                    detail.Tax = detail.PurchasePrice * taxRate;
                }

                details.Add(detail);
            }

            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };
            sale.Total = sale.SubTotal + sale.Tax;

            //Save SaleModel
            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData<SaleDBModel>("dbo.spSale_Insert", sale, "AppData");

            //Get Id from SaleModel
            sale.Id = sql.LoadData<int, dynamic>("dbo.spSaleIdLookup", new { CashierId = sale.CashierId, SaleDate = sale.SaleDate }, "AppData").FirstOrDefault();

            //Finish filling in the SaleDetailModel

            foreach (var product in details)
            {
                product.SaleId = sale.Id;
                sql.SaveData<SaleDetailDBModel>("dbo.spSaleDetail_Insert", product, "AppData");
            }
        }
    }
}
