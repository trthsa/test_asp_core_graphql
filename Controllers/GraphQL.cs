using Microsoft.EntityFrameworkCore;

public class Query
{
    public Book?[] GetBooks([Service] ApplicationDbContext dbContext)
    {
        //Get all books with their authors
        return dbContext.Books.Include(b => b.Author).ToArray();
    }
}

//CREATE A BOOK
public class Mutation
{
    public Task<Book> CreateBook(string title, string authorName, [Service] ApplicationDbContext dbContext)
    {
        var book = new Book
        {
            Title = title,
            Author = new Author
            {
                Name = authorName
            }
        };
        dbContext.Books.Add(book);
        dbContext.SaveChanges();

        return Task.FromResult(book);
    }

}
