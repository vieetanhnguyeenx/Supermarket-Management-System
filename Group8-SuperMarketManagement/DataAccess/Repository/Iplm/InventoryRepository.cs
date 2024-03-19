using AutoMapper;
using BusinessObject;
using DataAccess.DAO;
using DataAccess.DTOs;
using DataAccess.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Iplm
{
    public class InventoryRepository : IInventoryRepository
    {
        private IMapper mapper;

        public InventoryRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationMapper());
            });
            mapper = config.CreateMapper();
        }
        public void DeleteInventory(int id)
        {
           InventoryDAO.DeleteInventory(id);
        }

        public List<InventoryDTORespone> GetInventories()
        {
            return mapper.Map<List<Inventory>, List<InventoryDTORespone>>(InventoryDAO.GetInventories());
        }

        public InventoryDTORespone GetInventoryById(int id)
        {
            return mapper.Map<Inventory, InventoryDTORespone>(InventoryDAO.GetInventoryById(id));
        }

        public void SaveInventory(InventoryDTOCreate inventory)
        {
            InventoryDAO.SaveInventory(mapper.Map<InventoryDTOCreate, Inventory>(inventory));
        }
    }
}
