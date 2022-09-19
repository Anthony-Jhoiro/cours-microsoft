using Leonardo;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/fibonacci", () => Fibonacci.RunAsync(new[] { "44", "45" }));

app.Run();
