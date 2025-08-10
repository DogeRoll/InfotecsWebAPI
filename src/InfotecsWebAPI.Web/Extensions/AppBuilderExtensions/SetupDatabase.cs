using InfotecsWebAPI.Application.Abstractions;
using InfotecsWebAPI.Application.Exceptions;

namespace InfotecsWebAPI.Web.Extensions
{
    public static partial class AppBuilderExtensions
    {
        /// <summary>
        /// Creates tables in database
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder SetupDatabase(this IApplicationBuilder app)
        {
            var loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();
            var logger = loggerFactory!.CreateLogger("SetupDatabase");

            using var scope = app.ApplicationServices.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

            try
            {
                dbInitializer.Initialize();
            }
            catch (DatabaseException ex)
            {
                logger.LogCritical(ex, "Could not initialize database");
                throw;
            }

            return app;
        }
    }
}
