using DatasetsBackend.Dtos;
using DatasetsBackend.Validators;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DatasetsBackend_Tests
{
    public class DatasetValidator_Tests
    {
        IFormFile File;

        public DatasetValidator_Tests()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            File = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.txt");
        }

        [Fact]
        public void NameIsLatin()
        {
            var dto = new UploadDatasetDto
            {
                Name = "Test",
                ContainsCyrillic = true
            };

            var validator = new DatasetValidator(dto, File);

            var validationErrors = validator.GetValidationErrors();

            Assert.Empty(validationErrors);
        }

        [Fact]
        public void NameIsNotLatin()
        {
            var dto = new UploadDatasetDto
            {
                Name = "teыt"
            };

            var validator = new DatasetValidator(dto, File);

            var validationErrors = validator.GetValidationErrors();

            Assert.Collection(validationErrors, item => Assert.Contains("Name should contain only latin chars", item));
        }

        [Fact]
        public void NameContainsDigit()
        {
            var dto = new UploadDatasetDto
            {
                Name = "Test1"
            };

            var validator = new DatasetValidator(dto, File);

            var validationErrors = validator.GetValidationErrors();

            Assert.Collection(validationErrors, item => Assert.Contains("Name should contain only latin chars", item));
        }

        [Fact]
        public void NameContainsCaptcha()
        {
            var dto = new UploadDatasetDto
            {
                Name = "CaPtChas"
            };

            var validator = new DatasetValidator(dto, File);

            var validationErrors = validator.GetValidationErrors();

            Assert.Collection(validationErrors, item => Assert.Contains("Name should not contain word captcha", item));
        }

        [Fact]
        public void NameLengthInvalid()
        {
            var dto = new UploadDatasetDto
            {
                Name = new string('a', 10)
            };

            var validator = new DatasetValidator(dto, File);

            var validationErrors = validator.GetValidationErrors();

            Assert.Collection(validationErrors, item => Assert.Contains("Name length should be between 4 and 8", item));
        }

        [Fact]
        public void ChosenCharTypesInvalid()
        {
            var dto = new UploadDatasetDto
            {
                Name = "Test"
            };

            var validator = new DatasetValidator(dto, File);

            var validationErrors = validator.GetValidationErrors();

            Assert.Collection(validationErrors, item => Assert.Contains("Dataset should contain cyrillic/latin chars or digits", item));
        }
    }
}
