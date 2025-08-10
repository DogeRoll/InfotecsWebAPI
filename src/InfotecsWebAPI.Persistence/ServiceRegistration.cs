using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfotecsWebAPI.Application.Abstractions;
using InfotecsWebAPI.Application.Abstractions.Repositories;
using InfotecsWebAPI.Persistence.Context;
using InfotecsWebAPI.Persistence.Repositories;
using InfotecsWebAPI.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InfotecsWebAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var cfg = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(cfg))
                throw new InvalidOperationException("Could not read persistance configuration");

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(cfg));

            services.AddScoped<ITimescaleRepository, EfTimescalesRepository>();
            services.AddScoped<IDbInitializer, EfInitializer>();
        }
    }
}
