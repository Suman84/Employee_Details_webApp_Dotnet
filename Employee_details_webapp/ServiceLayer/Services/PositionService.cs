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
    public class PositionService : IPositionService
    {
        #region Property
        private IPositionRepository<Positions> _repository;
        #endregion

        #region Constructor
        public PositionService(IPositionRepository<Positions> repository)
        {
            _repository = repository;
        }
        #endregion

        public IEnumerable<Positions> GetAllPositions()
        {
            return _repository.GetAll();
        }

        public Positions GetPosition(int id)
        {
            return _repository.Get(id);
        }

        public void InsertPosition(Positions positions)
        {
            _repository.Insert(positions);
        }
        public void UpdatePosition(Positions positions)
        {
            _repository.Update(positions);
        }

        public void DeletePosition(int id)
        {
            Positions positions = GetPosition(id);
            _repository.Delete(positions);
            _repository.SaveChanges();
        }
    }
}
