using DatasetsBackend.Dtos;
using DatasetsBackend.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using Xunit;

namespace DatasetsBackend_Tests
{
    public class CustomDeserializer_Tests
    {
        [Fact]
        public void ValidModel()
        {
            var dictionary = new Dictionary<string, StringValues>();

            var createdDate = "2022-01-05T12:02:00";

            dictionary.Add("Name", "Dataset name");
            dictionary.Add("CreateDate", createdDate);
            dictionary.Add("ContainsCyrillic", "true");
            dictionary.Add("ContainsLatin", "true");
            dictionary.Add("ContainsDigits", "false");
            dictionary.Add("ContainsSpecChars", "false");
            dictionary.Add("CaseSensitive", "true");
            dictionary.Add("AnswersLocation", "SeparateFile");

            var form = new FormCollection(dictionary);

            var deserilizedModel = CustomDeserializer.DeserializeForm<UploadDatasetDto>(form);

            var accualDate = DateTime.Parse(createdDate);
            Assert.Equal("Dataset name", deserilizedModel.Name);
            Assert.Equal(accualDate, deserilizedModel.CreateDate);
            Assert.True(deserilizedModel.ContainsCyrillic);
            Assert.True(deserilizedModel.ContainsLatin);
            Assert.False(deserilizedModel.ContainsDigits);
            Assert.False(deserilizedModel.ContainsSpecChars);
            Assert.True(deserilizedModel.CaseSensitive);
            Assert.Equal(DatasetsBackend.Data.AnswersLocation.SeparateFile, deserilizedModel.AnswersLocation);
        }
        [Fact]
        public void InvalidDate()
        {
            var dictionary = new Dictionary<string, StringValues>();

            var invalidDate = "!2022-01-05T12:02:00";

            dictionary.Add("Name", "Dataset name");
            dictionary.Add("CreateDate", invalidDate);
            dictionary.Add("ContainsCyrillic", "true");
            dictionary.Add("ContainsLatin", "true");
            dictionary.Add("ContainsDigits", "false");
            dictionary.Add("ContainsSpecChars", "false");
            dictionary.Add("CaseSensitive", "true");
            dictionary.Add("AnswersLocation", "SeparateFile");

            var form = new FormCollection(dictionary);

            var exception = Assert.Throws<ArgumentException>(() => CustomDeserializer.DeserializeForm<UploadDatasetDto>(form));

            Assert.Equal("CreateDate is in wrong format", exception.Message);
        }
        [Fact]
        public void InvalidBool()
        {
            var dictionary = new Dictionary<string, StringValues>();

            var invalidDate = "2022-01-05T12:02:00";

            dictionary.Add("Name", "Dataset name");
            dictionary.Add("CreateDate", invalidDate);
            dictionary.Add("ContainsCyrillic", "da");
            dictionary.Add("ContainsLatin", "true");
            dictionary.Add("ContainsDigits", "false");
            dictionary.Add("ContainsSpecChars", "false");
            dictionary.Add("CaseSensitive", "true");
            dictionary.Add("AnswersLocation", "SeparateFile");

            var form = new FormCollection(dictionary);

            var exception = Assert.Throws<ArgumentException>(() => CustomDeserializer.DeserializeForm<UploadDatasetDto>(form));

            Assert.Equal("ContainsCyrillic is in wrong format", exception.Message);
        }
        [Fact]
        public void InvalidEnum()
        {
            var dictionary = new Dictionary<string, StringValues>();

            var invalidDate = "2022-01-05T12:02:00";

            dictionary.Add("Name", "Dataset name");
            dictionary.Add("CreateDate", invalidDate);
            dictionary.Add("ContainsCyrillic", "true");
            dictionary.Add("ContainsLatin", "true");
            dictionary.Add("ContainsDigits", "false");
            dictionary.Add("ContainsSpecChars", "false");
            dictionary.Add("CaseSensitive", "true");
            dictionary.Add("AnswersLocation", "No_File");

            var form = new FormCollection(dictionary);

            var exception = Assert.Throws<ArgumentException>(() => CustomDeserializer.DeserializeForm<UploadDatasetDto>(form));

            Assert.Equal("AnswersLocation is in wrong format", exception.Message);
        }
    }
}