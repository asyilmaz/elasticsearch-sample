using ElasticSample.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSample
{
    public class ElasticContext : DbContext
    {
        public ElasticContext(DbContextOptions<ElasticContext> options) : base(options)
        { }

        public DbSet<Employee> Employee { get; set; }
    }
}
