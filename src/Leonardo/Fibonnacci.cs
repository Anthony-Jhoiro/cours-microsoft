using Microsoft.EntityFrameworkCore;

namespace Leonardo;

record ResultTuple(long Input, long Output, bool FromCache);

public class Fibonacci
{
    private readonly FibonacciDataContext _fibonacciDataContext;

    public Fibonacci(FibonacciDataContext fibonacciDataContext)
    {
        _fibonacciDataContext = fibonacciDataContext;
    }


    public static long Run(int index)
    {
        var previousBuff = 0L;
        var buff = 1L;
        for (var i = 1; i < index; i++)
        {
            (buff, previousBuff) = (buff + previousBuff, buff);
        }

        Console.WriteLine($"Fibonacci({index}) = {buff}");

        return buff;
    }

    public async Task<List<long>> RunAsync(string[] args)
    {
        var tasks = new List<Task<ResultTuple>>();
        foreach (var arg in args)
        {
            var int32 = Convert.ToInt32(arg);
            var queryResult = await _fibonacciDataContext.TFibonaccis.Where(f => f.FibInput == int32)
                .Select(f => f.FibOutput).FirstOrDefaultAsync();

            if (queryResult == default)
            {
                var result = Task.Run(() =>new ResultTuple(int32, Run(int32), false));
                tasks.Add(result);
            }
            else
            {
                tasks.Add(Task.FromResult(new ResultTuple(int32, queryResult, true)));
            }
        }

        Task.WaitAll(tasks.ToArray());

        var results = tasks.Select(t => t.Result).ToList();

        foreach (var result in results)
        {
            if (!result.FromCache)
            {
                _fibonacciDataContext.TFibonaccis.Add(new TFibonacci()
                {
                    FibOutput = result.Output,
                    FibCreatedTimestamp = DateTime.Now,
                    FibInput = result.Input,
                });
            }
        }

        await _fibonacciDataContext.SaveChangesAsync();

        return results.Select(r => r.Output).ToList();
    }
}