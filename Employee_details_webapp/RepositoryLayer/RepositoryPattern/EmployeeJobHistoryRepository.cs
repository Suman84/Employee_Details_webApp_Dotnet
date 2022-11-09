using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DataAccess;
using RepositoryLayer.RepositoryPattern.Interfaces;

namespace RepositoryLayer.RepositoryPattern
{
    public class EmployeeJobHistoryRepository<T> : IEmployeeJobHistoryRepository<T> where T:EmployeeJobHistories
    {
        #region property
        private readonly EmployeeDbContext _employeeDbContext;
        private DbSet<T> entities;
        #endregion

        #region Constructor
        public EmployeeJobHistoryRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
            entities = _employeeDbContext.Set<T>();
        }
        #endregion


        public T Get(Guid Id)
        {
            return entities.SingleOrDefault(c => c.EmployeeJobHistoryid == Id);
        }

        public T GetUsingEmployeeid(Guid Id)
        {
            return entities.Where(x => x.Employeeid == Id).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _employeeDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            _employeeDbContext.SaveChanges();
        }

        public void Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _employeeDbContext.SaveChanges();
        }

        public void SaveChanges()
        {
            _employeeDbContext.SaveChanges();
        }


    }
}
