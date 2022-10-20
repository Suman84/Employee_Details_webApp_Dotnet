using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IEmployeeJobHistoryService
    {
        IEnumerable<EmployeeJobHistories> GetAllEmployeeJobHistories();
        EmployeeJobHistories GetEmployeeJobHistory(int id);
        void InsertEmployeeJobHistory(EmployeeJobHistories employeeJobHistory);
        void UpdateEmployeeJobHistory(EmployeeJobHistories employeeJobHistory);
        void DeleteEmployeeJobHistory(int id);

    }
}
