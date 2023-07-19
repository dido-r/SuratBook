namespace SuratBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static SuratBook.Data.Constants.Constants;

    public class Education
    {
        public Guid Id { get; set; }

        [StringLength(UniversityNameMaxLength, MinimumLength = UniversityNameMinLength)]
        public string University { get; set; } = null!;

        [ForeignKey(nameof(UniversityDegree))]
        public int UniversityDegreeId { get; set; }

        public UniversityDegree UniversityDegree { get; set; } = null!;

        [StringLength(SchoolNameMaxLength, MinimumLength = SchoolNameMinLength)]
        public string School { get; set; } = null!;

    }
}
