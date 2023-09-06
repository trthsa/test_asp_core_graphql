using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Define DbSet properties for your entities
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Author { get; set; }

}

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }

    public Author? Author { get; set; }
}

public class Author

{
    public int Id { get; set; }
    public string? Name { get; set; }
}