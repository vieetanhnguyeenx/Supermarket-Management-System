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
    public class SalesTransactionRepository : ISalesTransactionRepository
    {
        private IMapper mapper;
        public SalesTransactionRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationMapper());
            });
            mapper = config.CreateMapper();
        }
        public void DisableTransaction(int transactionID)
        {
            SalesTransactionDAO.DisableSalesTransaction(transactionID);
        }

        public List<SalesTransactionDTOResponse> GetAllTransactions()
        {
            return mapper.Map<List<SalesTransaction>, List<SalesTransactionDTOResponse>>(SalesTransactionDAO.GetSalesTransactions());
        }

        public List<SalesTransactionDTOResponse> GetAllTransactionsByEmployeeID(string employeeID)
        {
            return mapper.Map<List<SalesTransaction>, List<SalesTransactionDTOResponse>>(SalesTransactionDAO.GetSalesTransactionsByEmployeeID(employeeID));
        }

      
        public SalesTransactionDTOResponse GetTransaction(int id)
        {
            return mapper.Map<SalesTransaction, SalesTransactionDTOResponse>(SalesTransactionDAO.GetTransactionByID(id));
        }

        public SalesTransaction SaveTransaction(SalesTransactionDTOPOST salesTransaction)
        {
            return SalesTransactionDAO.SaveSalesTransaction(mapper.Map<SalesTransaction>(salesTransaction));
        }
    }
}

