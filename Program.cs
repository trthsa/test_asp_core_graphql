using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using dotenv.net;
// Load environment variables from the .env file
DotEnv.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

// Configure your database context here
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionStringFromEnv = Environment.GetEnvironmentVariable("CONNECTION_STRING");
Console.WriteLine($"Connection string from env: {connectionStringFromEnv}");
if (!string.IsNullOrEmpty(connectionStringFromEnv))
{
    connectionString = connectionStringFromEnv;
}


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
    .WithOrigins("https://studio.apollographql.com", "http://localhost:3000") // Add your allowed origins here
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.Run();
