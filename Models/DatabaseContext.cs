using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TamagotchiAPI.Models
{
    public partial class DatabaseContext : DbContext
    {
        //Add database table models here:
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Playtime> Playtimes { get; set; }
        public DbSet<Feeding> Feedings { get; set; }
        public DbSet<Scolding> Scoldings { get; set; }

        // Change this if you want to have a different database name in development
        private static string DEVELOPMENT_DATABASE_NAME = "TamagotchiAPIDatabase";

        // Change this to true if you want to have logging of SQL statements in development
        private static bool LOG_SQL_STATEMENTS_IN_DEVELOPMENT = false;

        // Add database tables here:


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (LOG_SQL_STATEMENTS_IN_DEVELOPMENT && Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                optionsBuilder.UseLoggerFactory(loggerFactory);
            }

            if (!optionsBuilder.IsConfigured)
            {
                var databaseURL = Environment.GetEnvironmentVariable("DATABASE_URL");
                var defaultConnectionString = $"server=localhost;database={DEVELOPMENT_DATABASE_NAME}";

                var conn = databaseURL != null ? ConvertPostConnectionToConnectionString(databaseURL) : defaultConnectionString;

                //Error message having to do with "TimeStamp" formatting. Fixed issue with:
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

                optionsBuilder.UseNpgsql(conn);
            }
        }

        private string ConvertPostConnectionToConnectionString(string connection)
        {
            var _connection = connection.Replace("postgres://", String.Empty);

            var connectionParts = Regex.Split(_connection, ":|@|/");

            return $"server={connectionParts[2]};SSL Mode=Require;Trust Server Certificate=true;database={connectionParts[4]};User Id={connectionParts[0]};password={connectionParts[1]};port={connectionParts[3]}";
        }
    }
}
