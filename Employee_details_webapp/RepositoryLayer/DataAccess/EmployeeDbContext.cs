using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.DataAccess
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<People> Peoples { get; set; }

        public DbSet<Employees> Employees { get; set; }

        public DbSet<Positions> Positions { get; set; }

        public DbSet<EmployeeJobHistories> EmployeeJobHistories { get; set; }

    }
}
