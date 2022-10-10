// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Channels;
using Leonardo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


// T ControlTime<T>(Func<T> a)
// {
//     var w = new Stopwatch();
//     w.Start();
//     var ret = a();
//
//     w.Stop();
//     var ts = w.Elapsed;
//     var elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
//
//     Console.WriteLine($"Elapsed time : {elapsedTime}");
//     return ret;
// }

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
    .Build();


var services = new ServiceCollection();

services.AddSingleton(implementationFactory: _ =>
{
    var builder = new DbContextOptionsBuilder<FibonacciDataContext>();
    var dataBaseName = Guid.NewGuid().ToString();
    builder.UseInMemoryDatabase(dataBaseName);
    var options = builder.Options;
    return new FibonacciDataContext(options);
});
services.AddDbContext<FibonacciDataContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
services.AddTransient<Fibonacci>();
services.AddLogging(configure => configure.AddFilter("Microsoft", LogLevel.Warning)
    .AddFilter("System", LogLevel.Warning)
    .AddFilter("Demo", LogLevel.Debug)
    .AddConsole());

await using var serviceProvider = services.BuildServiceProvider();
{
    var fibonacciDataContext = serviceProvider.GetService<FibonacciDataContext>();
    await fibonacciDataContext.Database.EnsureCreatedAsync();

    var applicationSection = configuration.GetSection("Application");
    var applicationConfig = applicationSection.Get<ApplicationConfig>();

    var logger = serviceProvider.GetService<ILogger<Program>>();

    logger.LogInformation($"Application Name : {applicationConfig.Name}");
    logger.LogInformation($"Application Message : {applicationConfig.Message}");


    var a = await serviceProvider.GetService<Fibonacci>().RunAsync(args);


    logger.LogInformation($"res: {a.Select(a => a.ToString()).Aggregate((aaa, bbb) => $"{aaa}, {bbb}")}");
}

public class ApplicationConfig
{
    public string Name { get; set; }
    public string Message { get; set; }
}