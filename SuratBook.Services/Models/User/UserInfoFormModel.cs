namespace SuratBook.Services.Models.User
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    using static SuratBook.Data.Constants.Constants;
    using static SuratBook.Data.Constants.ErrorMessages;

    public class UserInfoFormModel
    {
        [AllowNull]
        [StringLength(CountryMaxLength, ErrorMessage = CountryErrorMessage)]
        public string Country { get; set; }

        [AllowNull]
        [StringLength(TownMaxLength, ErrorMessage = TownErrorMessage)]
        public string Town { get; set; }

        [AllowNull]
        [StringLength(AddressMaxLength, ErrorMessage = AddressErrorMessage)]
        public string Address { get; set; }

        [AllowNull]
        [StringLength(UniversityNameMaxLength, ErrorMessage = UniversityErrorMessage)]
        public string University { get; set; }

        [AllowNull]
        public int UniversityDegreeId { get; set; }

        [AllowNull]
        [StringLength(SchoolNameMaxLength, ErrorMessage = SchoolErrorMessage)]
        public string School { get; set; }
    }
}
