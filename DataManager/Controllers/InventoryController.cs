using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataManager.Library.DataAccess;
using DataManager.Library.Models;

namespace DataManager.Controllers
{
    public class InventoryController : ApiController
    {
        [HttpGet]
        public List<InventoryModel> Get()
        {
            InventoryData data = new InventoryData();

            return data.GetInventory();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public void Post(InventoryModel products)
        {
            InventoryData data = new InventoryData();

            data.SaveInventoryRecord(products);
        }
    }
}
