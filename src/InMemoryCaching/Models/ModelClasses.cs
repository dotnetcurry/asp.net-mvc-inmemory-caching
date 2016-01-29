using System.Collections.Generic;

namespace InMemoryCaching.Models
{
    public class Employee
    {
        public int EmpNo { get; set; }
        public string EmpName { get; set; }
    }
    public class EmployeesDatabase : List<Employee>
    {
        public EmployeesDatabase()
        {
            Add(new Employee() { EmpNo = 1, EmpName = "A" });
            Add(new Employee() { EmpNo = 2, EmpName = "B" });
            Add(new Employee() { EmpNo = 3, EmpName = "C" });
            Add(new Employee() { EmpNo = 4, EmpName = "D" });
            Add(new Employee() { EmpNo = 5, EmpName = "E" });
            Add(new Employee() { EmpNo = 6, EmpName = "F" });
        }
    }

    public class DataAccess
    {
        public List<Employee> Get()
        {
            return new EmployeesDatabase();
        }
    }
}
