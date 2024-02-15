using System.Collections.Generic;
using DataManager.Library.Models;

namespace DataManager.Library.DataAccess
{
    public interface IInventoryData
    {
        List<InventoryModel> GetInventory();
        void SaveInventoryRecord(InventoryModel products);
    }
}