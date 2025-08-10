using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using InfotecsWebAPI.Application.Abstractions;
using InfotecsWebAPI.Application.Abstractions.CsvParser;
using InfotecsWebAPI.Application.Dtos.CsvParsing;

namespace InfotecsWebApi.SepCsvParser
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("CsvParserSettings");
            if (!section.Exists())
                throw new InvalidOperationException("Could not find CfgParserSettings section");

            services.Configure<CsvParserSettings>(section);
            services.AddScoped<ICsvParser<TimescaleDto>, SepCsvParser>();
        }
    }
}
