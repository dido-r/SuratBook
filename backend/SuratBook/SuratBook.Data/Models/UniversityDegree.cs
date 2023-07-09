namespace SuratBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static SuratBook.Data.Constants.Constants;

    public class UniversityDegree
    {
        public int Id { get; set; }

        [Required]
        [StringLength(DegreeMaxLength, MinimumLength = DegreeMinLength)]
        public string Name { get; set; } = null!;
    }
}
