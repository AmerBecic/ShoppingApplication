using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataManager.Library.DataAccess;
using DataManager.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        [Authorize(Roles = "Cashier")]
        [HttpPost]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //RequestContext.Principal.Identity.GetUserId(); - From .Net Framework

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
