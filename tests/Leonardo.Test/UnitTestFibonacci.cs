using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Leonardo.Test;

public class UnitTestFibonacci
{
    [Fact]
    public async Task RunAsync6ShouldReturn8()
    {
        var builder = new DbContextOptionsBuilder<FibonacciDataContext>();
        var dataBaseName = Guid.NewGuid().ToString();
        builder.UseInMemoryDatabase(dataBaseName);

        var options = builder.Options;
        var fibonacciDataContext = new FibonacciDataContext(options);
        await fibonacciDataContext.Database.EnsureCreatedAsync(); 
        var result = await new Fibonacci(fibonacciDataContext, new Logger<Fibonacci>(new LoggerFactory())).RunAsync(new[] { "6" });
        Assert.Equal(8,  result[0]);
    }
}