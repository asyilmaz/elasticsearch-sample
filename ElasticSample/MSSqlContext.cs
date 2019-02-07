using ElasticSample.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSample
{
    public class MSSqlContext : DbContext
    {
        public MSSqlContext(DbContextOptions<MSSqlContext> options) : base(options)
        { }

        public DbSet<Employee> Employee { get; set; }
    }
}
