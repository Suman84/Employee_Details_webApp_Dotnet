using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace ServiceLayer.Interfaces
{
    public interface IPeopleService
    {
        IEnumerable<People> GetAllPeople();
        People GetPeople(Guid id);
        void InsertPeople(People people);
        void UpdatePeople(People people);
        void DeletePeople(Guid id);

    }
}
