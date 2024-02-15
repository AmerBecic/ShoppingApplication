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
    public class InventoryData : IInventoryData
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public InventoryData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public List<InventoryModel> GetInventory()
        {
            var output = _sqlDataAccess.LoadData<InventoryModel, dynamic>("dbo.spInventory_GetAll", new { }, "AppData");

            return output;
        }

        public void SaveInventoryRecord(InventoryModel products)
        {
            _sqlDataAccess.SaveData<InventoryModel>("dbo.spInventory_Insert", products, "AppData");
        }
    }
}
