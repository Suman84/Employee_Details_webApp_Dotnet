using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using RepositoryLayer.RepositoryPattern;
using RepositoryLayer.RepositoryPattern.Interfaces;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        #region Property
        private IEmployeeRepository<Employees> _repository;
        #endregion

        #region Constructor
        public EmployeeService(IEmployeeRepository<Employees> repository)
        {
            _repository = repository;
        }
        #endregion

        public IEnumerable<Employees> GetAllEmployees()
        {
            return _repository.GetAll();
        }

        public Employees GetEmployee(int id)
        {
            return _repository.Get(id);
        }

        public void InsertEmployee(Employees employees)
        {
            _repository.Insert(employees);
        }
        public void UpdateEmployee(Employees employees)
        {
            _repository.Update(employees);
        }

        public void DeleteEmployee(int id)
        {
            Employees employees = GetEmployee(id);
            _repository.Delete(employees);
            _repository.SaveChanges();
        }
    }
}
