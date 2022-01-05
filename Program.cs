using DatasetsBackend.Controllers;
using DatasetsBackend.Data;
using DatasetsBackend.Dtos;
using Microsoft.EntityFrameworkCore;


namespace DatasetsBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = BuildWebApplication(args);

            app.MapPost("/api/dataset/upload", DatasetsController.Upload).Accepts<UploadDatasetDto>("multipart/form-data");

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

            builder.Services.AddDbContext<DatasetesBackendDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddEndpointsApiExplorer();

            return builder.Build();
        }
    }
}
