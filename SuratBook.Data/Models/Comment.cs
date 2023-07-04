namespace SuratBook.Data.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string Content { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public Guid OwmerId { get; set; }

        public SuratUser Owner { get; set; } = null!;

        public Guid PostId { get; set; }

        public Post Post { get; set; } = null!;

        public Guid PhotoId { get; set; }

        public Photo Photo { get; set; } = null!;
    }
}
