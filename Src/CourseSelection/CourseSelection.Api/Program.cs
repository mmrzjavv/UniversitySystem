using CourseSelection.Api;
using CourseSelection.Application.Course;
using CourseSelection.Domain;
using CourseSelection.infrastructure.Persistance;
using CourseSelection.infrastructure.Service;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddDbConfig();
builder.AddServices();
builder.AddVersioningConfig();

// Test database connection during startup
TestDatabaseConnection(builder.Configuration.GetConnectionString("DefaultConnection"));

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Function to test the database connection
void TestDatabaseConnection(string? connString)
{
    try
    {
        Console.WriteLine("Testing database connection...");
        using (var connection = new SqlConnection(connString))
        {
            connection.Open();
            Console.WriteLine("Connection to database is successful!");
        }
    }
    catch (SqlException ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }
}