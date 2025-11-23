using Serilog;

namespace PoolGame.WebAPI.Services
{
    public static class AppExtension
    {

        public static void SerilogConfiguration(this IHostBuilder host)
        {
            host.UseSerilog((context, LoggerConfig) =>
            {
                LoggerConfig.WriteTo.Console(outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} | ClientIP={ClientIP} {NewLine}{Exception}");
                LoggerConfig.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Month, retainedFileCountLimit: 12, outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} | ClientIP={ClientIP} {NewLine}{Exception}");
                LoggerConfig.Enrich.FromLogContext();



            });
        }
    }
}
