using Microsoft.EntityFrameworkCore;

namespace BenchmarkCodes.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Person> People { get; set; }

    public static DatabaseContext Create()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseSqlite("Data Source=ExemploData\\PersonDatabaseExemplo.db");

        var context = new DatabaseContext(optionsBuilder.Options);

        // if (context.Database.EnsureCreated())
        // {
        //     context.People.AddRange(
        //         new List<Person>
        //     {
        //     new Person {
        //         Name = "John",
        //         Surname = "Doe"
        //     },
        //     new Person {
        //         Name = "Jane",
        //         Surname = "Doe"
        //     },
        //     new Person {
        //         Name = "John",
        //         Surname = "Smith"
        //     }
        //     });
        //     context.SaveChanges();
        // }

        return context;
    }
}
