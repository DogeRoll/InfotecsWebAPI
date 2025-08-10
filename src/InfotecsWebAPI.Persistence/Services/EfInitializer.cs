using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfotecsWebAPI.Application.Abstractions;
using InfotecsWebAPI.Persistence.Context;
using InfotecsWebAPI.Application.Exceptions;

namespace InfotecsWebAPI.Persistence.Services
{
    public class EfInitializer : IDbInitializer
    {
        private AppDbContext _context;

        public EfInitializer(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            if (!await _context.Database.CanConnectAsync())
                throw new DatabaseConnectionException("Could not connect to database");
            try
            {
                await _context.Database.EnsureDeletedAsync();
                await _context.Database.EnsureCreatedAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Operation cancelled: {ex.Message}");
            }
        }

        public void Initialize()
        {
            if (!_context.Database.CanConnect())
                throw new DatabaseConnectionException("Could not connect to database");

            try
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Operation cancelled: {ex.Message}");
            }
        }
    }
}
