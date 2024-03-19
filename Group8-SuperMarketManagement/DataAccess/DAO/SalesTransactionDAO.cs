using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class SalesTransactionDAO
    {
        public static List<SalesTransaction> GetSalesTransactions()
        {
            var listST = new List<SalesTransaction>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listST = context.SalesTransactions.Include(x => x.Employee).Where(x => x.Discontinued == false).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listST;
        }

        public static List<SalesTransaction> GetSalesTransactionsByEmployeeID(string employeeID)
        {
            var listST = new List<SalesTransaction>();
            try
            {
                using (var context = new MyDBContext())
                {
                    listST = context.SalesTransactions.Include(x => x.Employee).Where(x => x.Discontinued == false && x.EmployeeID == employeeID).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listST;
        }

        public static SalesTransaction GetTransactionByID(int transactionID)
        {
            var st = new SalesTransaction();
            try
            {
                using (var context = new MyDBContext())
                {
                    st = context.SalesTransactions.Include(x => x.Employee).SingleOrDefault(x => x.Discontinued == false && x.TransactionID == transactionID);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return st;
        }

        public static SalesTransaction SaveSalesTransaction(SalesTransaction salesTransaction)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.SalesTransactions.Add(salesTransaction);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return salesTransaction;
        }

        public static void DisableSalesTransaction(int transactionID)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var st = context.SalesTransactions.Include(x => x.Employee).SingleOrDefault(x => x.Discontinued == false && x.TransactionID == transactionID);
                    st.Discontinued = true;
                    context.Entry(st).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
