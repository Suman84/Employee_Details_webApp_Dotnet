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
        Positions GetPosition(int id);
        void InsertPosition(Positions people);
        void UpdatePosition(Positions people);
        void DeletePosition(int id);

    }
}
