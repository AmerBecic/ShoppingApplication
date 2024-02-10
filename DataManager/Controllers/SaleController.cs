using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataManager.Library.DataAccess;
using DataManager.Library.Models;
using Microsoft.AspNet.Identity;

namespace DataManager.Controllers
{
    public class SaleController : ApiController
    {
        [Authorize]
        [HttpPost]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();

            string userId = RequestContext.Principal.Identity.GetUserId();

            data.SaveSale(sale, userId);
        }

        [HttpGet]
        [Route("api/Sale/GetSalesReport")]
        public List<SaleReportModel> GetSalesReport()
        {
            SaleData data = new SaleData();

            return data.GetSaleReport();
        }
    }
}
