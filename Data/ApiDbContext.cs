using Microsoft.EntityFrameworkCore;
using AutomatedGarage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedGarage.Data
{
    public class ApiDbContext : DbContext 
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }

    }
}
