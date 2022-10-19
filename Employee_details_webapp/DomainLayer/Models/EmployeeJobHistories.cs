using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class EmployeeJobHistories
    {
        public int EmployeeJobHistoryid { get; set; }
        public DateOnly StartDate { get; set; } = new DateOnly();
        public DateOnly EndDate { get; set; } = new DateOnly();

        public int Employeeid { get; set; }
        public int Positionid { get; set; }

        [ForeignKey("Employeeid")]
        public Employees employees { get; set; }

        [ForeignKey("PositionID")]
        public Positions positions { get; set; }

    }
}
