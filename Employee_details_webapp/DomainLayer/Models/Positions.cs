using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Positions
    {
        [Key]
        public Guid Positionid { get; set; }
        public string? PositionName { get; set; }

    }
}
