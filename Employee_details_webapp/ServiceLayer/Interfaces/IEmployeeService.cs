using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<Employees> GetAllEmployees();
        Employees GetEmployee(Guid id);
        void InsertEmployee(Employees employees);
        void UpdateEmployee(Employees employees);
        void DeleteEmployee(Guid id);

    }
}
