namespace SuratBook.Data.Constants
{
    public class Constants
    {
        public const int FirstNameMaxLength = 30;
        public const int FirstNameMinLength = 2;
        public const int LastNameMaxLength = 30;
        public const int LastNameMinLength = 2;
        public const string FirstNameErrorMessage = "First name must be between 2 and 3 symbols long";
        public const string LastNameErrorMessage = "Last name must be between 2 and 3 symbols long";

        public const int PostMaxLength = 800;
        public const int PostMinLength = 1;
        public const string PostErrorMessage = "Post must be between 1 and 800 symbols long";

        public const int CountryMaxLength = 50;
        public const string CountryErrorMessage = "Country must be up to 50 symbols long";
        public const int TownMaxLength = 50;
        public const string TownErrorMessage = "Town must be up to 50 symbols long";
        public const int AddressMaxLength = 150;
        public const string AddressErrorMessage = "Address must be up to 150 symbols long";

        public const int GroupNameMaxLength = 50;
        public const int GroupNameMinLength = 3;
        public const int GroupInfoMaxLength = 500;
        public const int GroupInfoMinLength = 10;

        public const int UniversityNameMaxLength = 50;
        public const string UniversityErrorMessage = "University name must be up to 50 symbols long";
        public const int SchoolNameMaxLength = 50;
        public const string SchoolErrorMessage = "School name must be up to 50 symbols long";

        public const int CommentMaxLength = 500;
        public const int CommentMinLength = 1;

        public const int GroupAccessMaxLength = 10;
        public const int GroupAccessMinLength = 3;

        public const int DegreeMaxLength = 10;
        public const int DegreeMinLength = 3;
    }
}
