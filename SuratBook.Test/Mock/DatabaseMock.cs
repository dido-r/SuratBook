namespace SuratBook.Test.Mock
{
    using Microsoft.EntityFrameworkCore;
    using SuratBook.Data;

    public static class DatabaseMock
    {
        public static SuratBookDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<SuratBookDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new SuratBookDbContext(dbOptions);
        }
    }
}
