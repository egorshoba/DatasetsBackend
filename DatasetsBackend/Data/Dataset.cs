namespace DatasetsBackend.Data
{
    public class Dataset
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public bool ContainsCyrillic { get; set; }

        public bool ContainsLatin { get; set; }

        public bool ContainsDigits { get; set; }

        public bool ContainsSpecChars { get; set; }

        public bool CaseSensitive { get; set; }

        public AnswersLocation AnswersLocation { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    }

    public enum AnswersLocation
    {
        None,
        FileName,
        SeparateFile
    }
}
