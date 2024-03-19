using BusinessObject;
using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IInventoryRepository
    {
        List<InventoryDTORespone> GetInventories();
        InventoryDTORespone GetInventoryById(int id);
        void SaveInventory(InventoryDTOCreate inventory);
        void DeleteInventory(int id);
    }
}
