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
    public class PeopleService : IPeopleService
    {
        #region Property
        private IPeopleRepository<People> _repository;
        #endregion

        #region Constructor
        public PeopleService(IPeopleRepository<People> repository)
        {
            _repository = repository;
        }
        #endregion

        public IEnumerable<People> GetAllPeople()
        {
            return _repository.GetAll();
        }

        public People GetPeople(int id)
        {
            return _repository.Get(id);
        }

        public void InsertPeople(People people)
        {
            _repository.Insert(people);
        }
        public void UpdatePeople(People people)
        {
            _repository.Update(people);
        }

        public void DeletePeople(int id)
        {
            People people = GetPeople(id);
            _repository.Delete(people);
            _repository.SaveChanges();
        }
    }
}
