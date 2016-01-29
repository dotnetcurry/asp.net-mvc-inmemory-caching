using InMemoryCaching.Models;
using System.Collections.Generic;

namespace InMemoryCaching.Services
{
    public interface IService<T> where T :class
    {
        IEnumerable<T> Get();
    }

    public class EmployeeService : IService<Employee>
    {
        DataAccess ds;
        public EmployeeService(DataAccess d)
        {
            ds = d;
        }
        public IEnumerable<Employee> Get()
        {
            return ds.Get();
        }
    }
}
