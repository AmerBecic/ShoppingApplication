﻿using System;
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
        private readonly IInventoryData _inventoryData;

        public InventoryController(IConfiguration config, IInventoryData inventoryData)
        {
            _inventoryData = inventoryData;
        }
            [Authorize(Roles = "Admin,Manager")]
            [HttpGet]
            public List<InventoryModel> Get()
            {
                return _inventoryData.GetInventory();
            }

            [Authorize(Roles = "Admin")]
            [HttpPost]
            public void Post(InventoryModel products)
            {
                _inventoryData.SaveInventoryRecord(products);
            }
        }
    }
