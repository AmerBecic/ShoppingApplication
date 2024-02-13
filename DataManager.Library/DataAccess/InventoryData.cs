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
    public class InventoryData
    {
        private readonly IConfiguration _config;

        public InventoryData(IConfiguration config)
        {
            _config = config;
        }
        public List<InventoryModel> GetInventory()
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var output = sql.LoadData<InventoryModel, dynamic>("dbo.spInventory_GetAll", new { }, "AppData");

            return output;
        }

        public void SaveInventoryRecord(InventoryModel products)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            sql.SaveData<InventoryModel>("dbo.spInventory_Insert", products, "AppData");
        }
    }
}
