namespace SuratBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static SuratBook.Data.Constants.Constants;

    public class Location
    {
        public Guid Id { get; set; }

        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength)]
        public string Country { get; set; } = null!;

        [StringLength(TownMaxLength, MinimumLength = TownMinLength)]
        public string Town { get; set; } = null!;

        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = null!;
    }
}
