// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Channels;
using Leonardo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;



var builder = new DbContextOptionsBuilder<FibonacciDataContext>();
var dataBaseName = Guid.NewGuid().ToString();
builder.UseInMemoryDatabase(dataBaseName);

var options = builder.Options;
var fibonacciDataContext = new FibonacciDataContext(options);
await fibonacciDataContext.Database.EnsureCreatedAsync(); 

Console.WriteLine("Hello, World!");

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
    .Build();
    
var applicationSection = configuration.GetSection("Application");
var applicationConfig = applicationSection.Get<ApplicationConfig>();

var loggerFactory = LoggerFactory.Create(builder => {
        builder.AddFilter("Microsoft", LogLevel.Warning)
            .AddFilter("System", LogLevel.Warning)
            .AddFilter("Demo", LogLevel.Debug)
            .AddConsole();
    }
);
var logger = loggerFactory.CreateLogger("Demo.Program");

logger.LogInformation($"Application Name : {applicationConfig.Name}");
logger.LogInformation($"Application Message : {applicationConfig.Message}");

T ControlTime<T>(Func<T> a)
{
    var w = new Stopwatch();
    w.Start();
    var ret = a();

    w.Stop();
    var ts = w.Elapsed;
    var elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";

    Console.WriteLine($"Elapsed time : {elapsedTime}");
    return ret;
}


var a = await new Fibonacci(fibonacciDataContext).RunAsync(args);



Console.WriteLine($"res: {a.Select(a  => a.ToString()).Aggregate((aaa, bbb) => $"{aaa}, {bbb}")}");



public class ApplicationConfig
{
    public string Name { get; set; }
    public string Message { get; set; }
}

