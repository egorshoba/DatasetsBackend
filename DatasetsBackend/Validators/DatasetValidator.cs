using DatasetsBackend.Dtos;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace DatasetsBackend.Validators
{
    public class DatasetValidator
    {
        readonly UploadDatasetDto DatasetMetadata;
        readonly IFormFile File;
        readonly List<string> Errors;
        List<string> FileNames;

        public DatasetValidator(UploadDatasetDto datasetMetadata, IFormFile file)
        {
            DatasetMetadata = datasetMetadata;
            File = file;
            Errors = new();
            FileNames = new();

            using var stream = File.OpenReadStream();
            using var archive = new ZipArchive(stream);
            foreach (var entry in archive.Entries)
                FileNames.Add(entry.Name);
            
        }

        public IEnumerable<string> GetValidationErrors()
        {
            NameIsLatin();
            DoesntContainCaptcha();
            NameLength();
            CharTypeChosen();
            DatasetSizeCheck();
            AnswerFileExists();
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


        void DatasetSizeCheck()
        {
            const int maxOffset = 1000;

            int minSize = GetMinDatasetSize();

            int maxSize = minSize + maxOffset;

            int actualSize = GetDatasetSize();

            if (actualSize < minSize
                || actualSize > maxSize)
                Errors.Add($"Dataset's size should be between {minSize} and {maxSize}");
        }

        void AnswerFileExists()
        {
            const string answerFile = "answers.txt";
            if (!FileNames.Contains(answerFile)
                && DatasetMetadata.AnswersLocation == Data.AnswersLocation.SeparateFile)
                Errors.Add($"Dataset should contain {answerFile}");
        }

        int GetMinDatasetSize()
        {
            const int startMinSize = 2000;
            const int modifier = 3000;

            int minSize = startMinSize;

            if (DatasetMetadata.ContainsCyrillic)
                minSize += modifier;

            if (DatasetMetadata.ContainsLatin)
                minSize += modifier;

            if (DatasetMetadata.ContainsDigits)
                minSize += modifier;

            if (DatasetMetadata.CaseSensitive)
                minSize += modifier;

            if (DatasetMetadata.ContainsSpecChars)
                minSize += modifier;

            return minSize;
        }

        public int GetDatasetSize() => FileNames.Count;



    }
}
