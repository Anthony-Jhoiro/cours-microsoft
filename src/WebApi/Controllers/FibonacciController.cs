using Leonardo;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FibonacciController : Controller
{
    private readonly Fibonacci _fibonacci;
    private readonly ILogger<FibonacciController> _logger;

    
    public FibonacciController(Fibonacci fibonacci, ILogger<FibonacciController> logger)
    {
        _fibonacci = fibonacci;
        _logger = logger;
    }

    [HttpPost]
    public Task<List<long>> Compute(string[] args)
    {
        var res  = _fibonacci.RunAsync(args);

        _logger.LogInformation($"res: {res.Result.Select(a => a.ToString()).Aggregate((aaa, bbb) => $"{aaa}, {bbb}")}");
        return res;
    }
}