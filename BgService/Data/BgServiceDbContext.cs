using BgService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BgService.Data
{
    public class BgServiceDbContext : DbContext
    {
        public BgServiceDbContext(DbContextOptions<BgServiceDbContext> options) : base(options)
        {

        }
        public DbSet<SimpleTable> SimpleTable { get; set; }

        public DbSet<Employee> Employee { get; set; }
    }
}
