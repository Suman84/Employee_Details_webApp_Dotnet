using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Employees
    {
        [Key]
        public int Employeeid { get; set; }
        public int Salary { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int EmployeeCode { get; set; }
        public Boolean ISDisabled { get; set; }

        public int Personid { get; set; }
        public int Positionid { get; set; }

        [ForeignKey("PersonID")]
        public People people { get; set; }

        [ForeignKey("PositionID")]
        public Positions positions { get; set; }

    }
}
