namespace SuratBook.Data.Models
{
    public class Education
    {
        public Guid Id { get; set; }

        public string University { get; set; } = null!;

        public UniversityDegree UniversityDegree { get; set; }

        public string School { get; set; } = null!;

    }
}
