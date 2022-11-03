using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class EmployeeJobHistories
    {
        [Key]
        public Guid EmployeeJobHistoryid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid Employeeid { get; set; }
        public Guid Positionid { get; set; }

        [ForeignKey("Employeeid")]
        public Employees employees { get; set; }

        [ForeignKey("Positionid")]
        public Positions positions { get; set; }

    }
}
