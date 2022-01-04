namespace DatasetsBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = BuildWebApplication(args);

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }

        public static WebApplication BuildWebApplication(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());

            IHostEnvironment env = builder.Environment;
            var jsonFileName = $"appsettings.{env.EnvironmentName}.json";
            builder.Configuration.AddJsonFile(jsonFileName, optional: true, reloadOnChange: true);

            var connectionString = builder.Configuration.GetConnectionString("DatasetsBackendDbContext");

            builder.Services.AddEndpointsApiExplorer();

            return builder.Build();
        }
    }
}
