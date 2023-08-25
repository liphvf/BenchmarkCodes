using BenchmarkCodes.Data;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;
using Microsoft.EntityFrameworkCore;

namespace BenchmarkCodes.BenchmarkTests;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net70)]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn(NumeralSystem.Arabic)]
[RPlotExporter]
public class TasksTests
{
    private readonly DatabaseContext _db;

    public TasksTests()
    {
        _db = DatabaseContext.Create();
    }

    [Arguments(true)]
    [Arguments(false)]
    [Benchmark(Description = "ValueAsync")]
    public async ValueTask<Person> GetPersonWithAsyncResponseUsingValueTaskAsync(bool sync)
    {
        if (sync)
        {
            return new Person
            {
                Name = "John",
                Surname = "Doe"
            };
        }

        var person = await _db.People.SingleAsync(p => p.Name == "John" && p.Surname == "Doe");
        return person;
    }

    [Arguments(true)]
    [Arguments(false)]
    [Benchmark(Description = "TaskAsync")]
    public async Task<Person> GetPersonWithAsyncResponseUsingTaskAsync(bool sync)
    {
        if (sync)
        {
            return new Person
            {
                Name = "John",
                Surname = "Doe"
            };
        }
        var person = await _db.People.SingleAsync(p => p.Name == "John" && p.Surname == "Doe");
        return person;
    }
}

