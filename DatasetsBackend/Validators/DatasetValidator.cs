using DatasetsBackend.Dtos;
using System.Text.RegularExpressions;

namespace DatasetsBackend.Validators
{
    public class DatasetValidator
    {
        readonly UploadDatasetDto DatasetMetadata;
        readonly IFormFile File;
        List<string> Errors;

        public DatasetValidator(UploadDatasetDto datasetMetadata, IFormFile file)
        {
            DatasetMetadata = datasetMetadata;
            File = file;
            Errors = new();
        }

        public IEnumerable<string> GetValidationErrors()
        {
            NameIsLatin();
            return Errors;
        }

        void NameIsLatin()
        {
            var regex = new Regex(@"^[A-z]+$", RegexOptions.IgnoreCase);
            if (!regex.IsMatch(DatasetMetadata.Name))
            {
                Errors.Add("Name should contain only latin chars");
            }
        }
    }
}
