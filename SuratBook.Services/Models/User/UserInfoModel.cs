namespace SuratBook.Services.Models.User
{
    public class UserInfoModel
    {
        public string Country { get; set; } = null!;

        public string Town { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string University { get; set; } = null!;

        public int UniversityDegreeId { get; set; }

        public string UniversityDegree { get; set; } = null!;

        public string School { get; set; } = null!;
    }
}
