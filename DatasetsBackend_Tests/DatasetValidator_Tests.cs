using DatasetsBackend.Dtos;
using DatasetsBackend.Validators;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Xunit;

namespace DatasetsBackend_Tests
{
    public class DatasetValidator_Tests
    {
        IFormFile File;

        public DatasetValidator_Tests()
        {
            var filePath = "oknew.zip";

            var stream = System.IO.File.OpenRead(filePath);

            File = new FormFile(stream, 0, stream.Length, "oknew.zip", "oknew.zip");
        }

        [Fact]
        public void AllValid()
        {
            var dto = new UploadDatasetDto
            {
                Name = "Test",
                ContainsCyrillic = true,
                CaseSensitive = true,
                ContainsLatin = true,
                AnswersLocation = DatasetsBackend.Data.AnswersLocation.SeparateFile
            };

            var validator = new DatasetValidator(dto, File);

            var validationErrors = validator.GetValidationErrors();

            Assert.Empty(validationErrors);
        }


        [Fact]
        public void DatasetTooLarge()
        {
            var dto = new UploadDatasetDto
            {
                Name = "Test",
                ContainsCyrillic = true,
                CaseSensitive = true
            };

            var validator = new DatasetValidator(dto, File);

            var validationErrors = validator.GetValidationErrors();

            Assert.Contains(validationErrors, m => m == "Dataset's size should be between 8000 and 9000");
        }
        [Fact]
        public void NameIsNotLatin()
        {
            var dto = new UploadDatasetDto
            {
                Name = "teыt",
                ContainsCyrillic = true,
                CaseSensitive = true,
                ContainsLatin = true,
                AnswersLocation = DatasetsBackend.Data.AnswersLocation.SeparateFile
            };

            var validator = new DatasetValidator(dto, File);

            var validationErrors = validator.GetValidationErrors();

            Assert.Contains(validationErrors, m => m == "Name should contain only latin chars");
        }

        [Fact]
        public void NameContainsDigit()
        {
            var dto = new UploadDatasetDto
            {
                Name = "Test1",
                ContainsCyrillic = true,
                CaseSensitive = true,
                ContainsLatin = true,
                AnswersLocation = DatasetsBackend.Data.AnswersLocation.SeparateFile
            };

            var validator = new DatasetValidator(dto, File);

            var validationErrors = validator.GetValidationErrors();

            Assert.Contains(validationErrors, m => m == "Name should contain only latin chars");
        }

        [Fact]
        public void NameContainsCaptcha()
        {
            var dto = new UploadDatasetDto
            {
                Name = "CaPtChas",
                ContainsCyrillic = true,
                CaseSensitive = true,
                ContainsLatin = true,
                AnswersLocation = DatasetsBackend.Data.AnswersLocation.SeparateFile
            };

            var validator = new DatasetValidator(dto, File);

            var validationErrors = validator.GetValidationErrors();

            Assert.Contains(validationErrors, m => m == "Name should not contain word captcha");
        }

        [Fact]
        public void NameLengthInvalid()
        {
            var dto = new UploadDatasetDto
            {
                Name = new string('a', 10),
                ContainsCyrillic = true,
                CaseSensitive = true,
                ContainsLatin = true,
                AnswersLocation = DatasetsBackend.Data.AnswersLocation.SeparateFile
            };

            var validator = new DatasetValidator(dto, File);

            var validationErrors = validator.GetValidationErrors();

            Assert.Contains(validationErrors, m => m == "Name length should be between 4 and 8");
        }

        [Fact]
        public void ChosenCharTypesInvalid()
        {
            var dto = new UploadDatasetDto
            {
                Name = "Test",             
                CaseSensitive = true,
                ContainsSpecChars = true,
                AnswersLocation = DatasetsBackend.Data.AnswersLocation.SeparateFile
            };

            var validator = new DatasetValidator(dto, File);

            var validationErrors = validator.GetValidationErrors();

            Assert.Contains(validationErrors, m => m == "Dataset should contain cyrillic/latin chars or digits");
        }
    }
}
