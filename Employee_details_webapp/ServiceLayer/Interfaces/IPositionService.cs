using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IPositionService
    {
        IEnumerable<Positions> GetAllPositions();
        Positions GetPosition(Guid id);
        void InsertPosition(Positions positions);
        void UpdatePosition(Positions positions);
        void DeletePosition(Guid id);

    }
}
