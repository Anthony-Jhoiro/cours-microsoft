using Leonardo;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FibonacciController : Controller
{

    [HttpPost]
    public Task<List<long>> Compute(
        [FromServices] Fibonacci fibonacci,
        [FromServices] ILogger<FibonacciController> logger,
        string[] args
    )
    {
        var res = fibonacci.RunAsync(args);

        logger.LogInformation($"res: {res.Result.Select(a => a.ToString()).Aggregate((aaa, bbb) => $"{aaa}, {bbb}")}");
        return res;
    }
}