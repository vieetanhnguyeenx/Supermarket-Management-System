using BusinessObject;

namespace DataAccess.DAO
{
    public class EmployeeDAO
    {
        public static List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();
            try
            {
                using (var context = new MyDBContext())
                {
                    employees = context.Employees.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return employees;
        }

        public static Employee GetEmployeeById(string id)
        {
            var employee = new Employee();
            try
            {
                using (var context = new MyDBContext())
                {
                    employee = context.Employees.FirstOrDefault(x => x.Id == id);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return employee;
        }

        public static void UpdateEmployee(Employee e)
        {
            /*
             * public string? LastName { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public DateTime? DoB { get; set; }
        public DateTime HireDate { get; set; }
        public string? Address { get; set; } = null!;
        public string? Phone { get; set; } = null!;
             */
            try
            {
                using (var context = new MyDBContext())
                {
                    var employee = context.Employees.FirstOrDefault(x => x.Id == e.Id);
                    employee.FirstName = e.FirstName;
                    employee.LastName = e.LastName;
                    employee.DoB = e.DoB;
                    employee.HireDate = e.HireDate;
                    employee.Address = e.Address;
                    employee.Phone = e.Phone;
                    context.Entry<Employee>(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteEmployee(string id)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var e = context.Employees.FirstOrDefault(x => x.Id == id);
                    e.Discontinued = true;
                    context.Entry<Employee>(e).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
