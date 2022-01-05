using DatasetsBackend.Dtos;
using System.IO.Compression;
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
            DoesntContainCaptcha();
            NameLength();
            CharTypeChosen();
            return Errors;
        }

        void NameIsLatin()
        {
            var regex = new Regex(@"^[A-z]+$", RegexOptions.IgnoreCase);
            if (!regex.IsMatch(DatasetMetadata.Name))
                Errors.Add("Name should contain only latin chars");
        }

        void DoesntContainCaptcha()
        {
            const string forbiddenWord = "captcha";
            if (DatasetMetadata.Name.ToLower().Contains(forbiddenWord))
                Errors.Add($"Name should not contain word {forbiddenWord}");
        }

        void NameLength()
        {
            const int minLength = 4;
            const int maxLength = 8;

            if (DatasetMetadata.Name.Length < minLength
                || DatasetMetadata.Name.Length > maxLength)
                Errors.Add($"Name length should be between {minLength} and {maxLength}");
        }

        void CharTypeChosen()
        {
            if (!DatasetMetadata.ContainsDigits 
                && !DatasetMetadata.ContainsCyrillic 
                && !DatasetMetadata.ContainsLatin)
            {
                Errors.Add("Dataset should contain cyrillic/latin chars or digits");
            }
        }
        public int GetDatasetSize()
        {
            using (var stream = File.OpenReadStream())
            using (var archive = new ZipArchive(stream))
            {
                return archive.Entries.Count;
            }
        }
    }
}
