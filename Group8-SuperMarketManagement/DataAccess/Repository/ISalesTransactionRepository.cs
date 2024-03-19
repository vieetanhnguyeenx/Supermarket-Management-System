using BusinessObject;
using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ISalesTransactionRepository
    {
        public SalesTransaction SaveTransaction(SalesTransactionDTOPOST salesTransaction);
        public SalesTransactionDTOResponse GetTransaction(int id);
        public List<SalesTransactionDTOResponse> GetAllTransactions();
        public List<SalesTransactionDTOResponse> GetAllTransactionsByEmployeeID(string employeeID);
        public void DisableTransaction(int transactionID);
    }
}
