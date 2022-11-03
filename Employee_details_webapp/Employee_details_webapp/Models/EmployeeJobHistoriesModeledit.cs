using DomainLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Employee_details_webapp.Models
{
	public class EmployeeJobHistoriesModeledit
	{
        [Key]
        public Guid EmployeeJobHistoryid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid Employeeid { get; set; }
        public Guid Positionid { get; set; }
        public string PositionName { get; set; }

    }
}
