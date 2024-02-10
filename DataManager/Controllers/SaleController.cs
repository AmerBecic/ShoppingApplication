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
    [Authorize]
    public class SaleController : ApiController
    {
        [Authorize(Roles = "Cashier")]
        [HttpPost]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();

            string userId = RequestContext.Principal.Identity.GetUserId();

            data.SaveSale(sale, userId);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        [Route("api/Sale/GetSalesReport")]
        public List<SaleReportModel> GetSalesReport()
        {
            //RequestContext.Principal.IsInRole("Admin");  --->  To check if whoever called this endpoint has Admin role 
            SaleData data = new SaleData();

            return data.GetSaleReport();
        }
    }
}
