namespace SuratBook.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static SuratBook.Data.Constants.Constants;

    public class Location
    {
        public Guid Id { get; set; }

        [StringLength(CountryMaxLength)]
        public string? Country { get; set; }

        [StringLength(TownMaxLength)]
        public string? Town { get; set; }

        [StringLength(AddressMaxLength)]
        public string? Address { get; set; }
    }
}