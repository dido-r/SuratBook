namespace SuratBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static SuratBook.Data.Constants.Constants;

    public class Education
    {
        public Guid Id { get; set; }

        [StringLength(UniversityNameMaxLength, MinimumLength = UniversityNameMinLength)]
        public string University { get; set; } = null!;

        public UniversityDegree UniversityDegree { get; set; }

        [StringLength(SchoolNameMaxLength, MinimumLength = SchoolNameMinLength)]
        public string School { get; set; } = null!;

    }
}
