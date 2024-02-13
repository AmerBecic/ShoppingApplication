using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManager.Library.DataAccess;
using DataManager.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IConfiguration _config;

        public InventoryController(IConfiguration config)
        {
            _config = config;
        }
            [Authorize(Roles = "Admin,Manager")]
            [HttpGet]
            public List<InventoryModel> Get()
            {
                InventoryData data = new InventoryData(_config);

                return data.GetInventory();
            }

            [Authorize(Roles = "Admin")]
            [HttpPost]
            public void Post(InventoryModel products)
            {
                InventoryData data = new InventoryData(_config);

                data.SaveInventoryRecord(products);
            }
        }
    }
