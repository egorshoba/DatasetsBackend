using DatasetsBackend.Data;

namespace DatasetsBackend.Dtos
{
    public class DatasetItem
    {
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }

        public DatasetItem(Dataset dataset)
        {
            CreateDate = dataset.CreateDate;
            Name = dataset.Name;
        }
    }
}
