namespace SuratBook.Data.Models
{
    public class Location
    {
        public Guid Id { get; set; }

        public string Country { get; set; } = null!;

        public string Town { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}
