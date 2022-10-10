using Leonardo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var services = new ServiceCollection();

services.AddSingleton(implementationFactory: _ =>
{
    var builder = new DbContextOptionsBuilder<FibonacciDataContext>();
    var dataBaseName = Guid.NewGuid().ToString();
    builder.UseInMemoryDatabase(dataBaseName);
    var options = builder.Options;

    return new FibonacciDataContext(options);
});
services.AddTransient<Fibonacci>();
services.AddLogging(configure => configure.AddConsole());

await using var serviceProvider = services.BuildServiceProvider();
{

    app.MapGet("/fibonacci", () => serviceProvider.GetService<Fibonacci>().RunAsync(new[] { "44", "45" }));

    app.Run();

}