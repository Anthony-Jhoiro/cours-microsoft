// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using System.Linq.Expressions;
using Leonardo;

Console.WriteLine("Hello, World!");

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

var tasks = args.Select(arg => Task.Run(() => ControlTime(() => Fibonacci.Run(Convert.ToInt32(arg))))).ToList();

ControlTime(() =>
{
    Task.WaitAll(tasks.ToArray());
    return 1;
});


