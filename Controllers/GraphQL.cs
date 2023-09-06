using HotChocolate.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class Query
{
    public Book?[] GetBooks([Service] ApplicationDbContext dbContext)
    {
        //Get all books with their authors
        return dbContext.Books.Include(b => b.Author).ToArray();
    }
    public User? GetUser()
    {
        // Validate user credentials
        var userId = "123"; // Replace with the actual user ID
        var username = "user@example.com"; // Replace with the actual username

        // Generate a JWT token
        var secretToken = Environment.GetEnvironmentVariable("SECRET_TOKEN") ?? throw new Exception("SECRET_TOKEN environment variable is not set.");

        var tokenService = new TokenService(secretToken);

        var token = tokenService.GenerateToken(userId, username);

        //Log out the token
        // Send the token as part of the login response
        return new User
        {
            Id = userId,
            Name = username,
            access_token = token
        };

    }


    public string DoSomeThing(ClaimsPrincipal claimsPrincipal)
    {
        //Get the user ID from the claims
        var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        Console.WriteLine($"User ID: {userId}");
        Console.WriteLine(claimsPrincipal.Identities.ToArray());
        //
        return "Hello World" + userId;
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
