using System.ComponentModel.DataAnnotations.Schema;

namespace SuratBook.Services.Models.User
{
    public class UserInfoFormModel
    {
        public string Country { get; set; } = null!;

        public string Town { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string University { get; set; } = null!;

        [ForeignKey(nameof(UniversityDegree))]
        public int UniversityDegreeId { get; set; }

        public UniversityDegreeViewModel UniversityDegree { get; set; } = null!;

        public string School { get; set; } = null!;

        public string UserId { get; set; } = null!;
    }
}
