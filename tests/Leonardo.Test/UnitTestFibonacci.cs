using Xunit;

namespace Leonardo.Test;

public class UnitTestFibonacci
{
    [Fact]
    public void RunAsync6ShouldReturn8()
    {
        var result = Fibonacci.RunAsync(new[] { "6" });
        Assert.Equal(8,  result[0]);
    }
}