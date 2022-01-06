using DatasetsBackend.Data;
using DatasetsBackend.Dtos;

namespace DatasetsBackend.Helpers
{
    public static class DatasetFactory
    {
        public static Dataset GetDataset(UploadDatasetDto dto)
        {
            return new()
            {
                CaseSensitive = dto.CaseSensitive,
                AnswersLocation = dto.AnswersLocation,
                ContainsCyrillic = dto.ContainsCyrillic,
                ContainsDigits = dto.ContainsDigits,
                ContainsLatin = dto.ContainsLatin,
                ContainsSpecChars = dto.ContainsSpecChars,
                CreateDate = dto.CreateDate,
                Name = dto.Name
            };
        }
    }
}
