using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using InfotecsWebAPI.Domain.Entities;
using InfotecsWebAPI.Application.Exceptions;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace InfotecsWebAPI.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        private ILogger<AppDbContext> _logger;

        public DbSet<TimescaleFile> Files { get; set; }
        public DbSet<TimescaleValues> Values { get; set; }
        public DbSet<TimescaleResults> Results { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, ILogger<AppDbContext> logger) : base(options)
        {
            _logger = logger;

            if (!Database.CanConnect())
            {
                var connString = Database.GetConnectionString();
                throw new DatabaseConnectionException($"Could not connect to postgres database with parameters: '{connString}'");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
