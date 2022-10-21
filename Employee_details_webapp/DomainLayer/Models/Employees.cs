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
        public Guid Employeeid { get; set; }
        public int Salary { get; set; } = 0;
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; } 
        public int EmployeeCode { get; set; } = 0;
        public Boolean ISDisabled { get; set; } = false;

        public Guid Personid { get; set; }
        public Guid Positionid { get; set; }

        [ForeignKey("Personid")]
        public People people { get; set; }

        [ForeignKey("Positionid")]
        public Positions positions { get; set; }

    }
}
