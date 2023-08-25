using BenchmarkCodes.Data;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Microsoft.EntityFrameworkCore;

namespace BenchmarkCodes.BenchmarkTests;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)]
// [SimpleJob(RuntimeMoniker.NativeAot70)]
[RPlotExporter]
public class TasksTests
{
    private readonly DatabaseContext _db;

    public TasksTests()
    {
        _db = DatabaseContext.Create();
    }

    [Benchmark(Description = "ValueSync")]
    public ValueTask<Person> GetPersonWithSyncResponseUsingValueTaskAsync()
    {
        var person = new Person
        {
            Name = "John",
            Surname = "Doe"
        };

        return new ValueTask<Person>(person);
    }

    [Benchmark(Description = "ValueAsync")]
    public async ValueTask<Person> GetPersonWithAsyncResponseUsingValueTaskAsync()
    {
        var person = await _db.People.SingleAsync(p => p.Name == "John" && p.Surname == "Doe");
        return person;
    }

    [Benchmark(Description = "TaskSync")]
    public Task<Person> GetPersonWithSyncResponseUsingTaskAsync()
    {
        var person = new Person
        {
            Name = "John",
            Surname = "Doe"
        };

        return Task.FromResult(person);
    }

    [Benchmark(Description = "TaskAsync")]
    public async Task<Person> GetPersonWithAsyncResponseUsingTaskAsync()
    {
        var person = await _db.People.SingleAsync(p => p.Name == "John" && p.Surname == "Doe");
        return person;
    }
}

