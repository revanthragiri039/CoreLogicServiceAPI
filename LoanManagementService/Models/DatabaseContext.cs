using LoanManagementService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManagementService.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Loan> Loans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=SURESH\SQLEXPRESS;Initial Catalog=CoreLogicDb;User ID=sa;Password=1234");
        }
    }
}
