using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using RepositoryLayer.RepositoryPattern.Interfaces;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services
{
    public class EmployeeJobHistoryService : IEmployeeJobHistoryService
    {
        #region Property
        private IEmployeeJobHistoryRepository<EmployeeJobHistories> _repository;
        #endregion

        #region Constructor
        public EmployeeJobHistoryService(IEmployeeJobHistoryRepository<EmployeeJobHistories> repository)
        {
            _repository = repository;
        }
        #endregion

        public IEnumerable<EmployeeJobHistories> GetAllEmployeeJobHistories()
        {
            return _repository.GetAll();
        }

        public EmployeeJobHistories GetEmployeeJobHistory(Guid id)
        {
            return _repository.Get(id);
        }

        public EmployeeJobHistories GetEmployeeJobHistoryUsingEmployeeid(Guid id)
        {
            return _repository.GetUsingEmployeeid(id);
        }

        public void InsertEmployeeJobHistory(EmployeeJobHistories employeeJobHistory)
        {
            _repository.Insert(employeeJobHistory);
        }
        public void UpdateEmployeeJobHistory(EmployeeJobHistories employeeJobHistory)
        {
            _repository.Update(employeeJobHistory);
        }

        public void DeleteEmployeeJobHistory(Guid id)
        {
            EmployeeJobHistories employeeJobHistory = GetEmployeeJobHistory(id);
            _repository.Delete(employeeJobHistory);
            _repository.SaveChanges();
        }
    }
}
