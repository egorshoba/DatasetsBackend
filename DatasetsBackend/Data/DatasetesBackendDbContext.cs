using Microsoft.EntityFrameworkCore;

namespace DatasetsBackend.Data
{
    public class DatasetesBackendDbContext : DbContext
    {
        public DatasetesBackendDbContext(DbContextOptions<DatasetesBackendDbContext> options)
            : base(options) { }

        public DbSet<Dataset> Datasets { get; set; }

    }
}
