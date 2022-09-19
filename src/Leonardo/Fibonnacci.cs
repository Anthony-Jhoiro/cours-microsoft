namespace Leonardo;

public static class Fibonacci
{
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
    
    public static List<long> RunAsync(IEnumerable<string> args)
    {
        var tasks = args.Select(arg => Task.Run(() => Run(Convert.ToInt32(arg)))).ToArray();
        Task.WaitAll(tasks);
            
        return tasks.Select(t=>t.Result).ToList();
    }
}