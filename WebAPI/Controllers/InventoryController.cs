using System;
using System.Collections.Generic;
using System.Linq;
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
    public class InventoryController : ControllerBase
    {
            [Authorize(Roles = "Admin,Manager")]
            [HttpGet]
            public List<InventoryModel> Get()
            {
                InventoryData data = new InventoryData();

                return data.GetInventory();
            }

            [Authorize(Roles = "Admin")]
            [HttpPost]
            public void Post(InventoryModel products)
            {
                InventoryData data = new InventoryData();

                data.SaveInventoryRecord(products);
            }
        }
    }
