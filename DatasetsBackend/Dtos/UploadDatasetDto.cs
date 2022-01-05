using DatasetsBackend.Data;

namespace DatasetsBackend.Dtos
{
    public record UploadDatasetDto
    {
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public bool ContainsCyrillic { get; set; }

        public bool ContainsLatin { get; set; }

        public bool ContainsDigits { get; set; }

        public bool ContainsSpecChars { get; set; }

        public bool CaseSensitive { get; set; }

        public AnswersLocation AnswersLocation { get; set; }
    }
}
