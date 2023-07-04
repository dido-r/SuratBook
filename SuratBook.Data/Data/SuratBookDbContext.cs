namespace SuratBook.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class SuratBookDbContext : IdentityDbContext
    {
        public SuratBookDbContext(DbContextOptions<SuratBookDbContext> options)
            : base(options)
        {
        }
    }
}