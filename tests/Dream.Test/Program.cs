using BenchmarkDotNet.Running;
using System;

namespace Dream.Test;

internal static class Program
{
    private static void Main()
    {
        //BenchmarkRunner.Run<>();
        Console.WriteLine("Benchmark completed. Press any key to exit.");
        Console.ReadKey();
    }
}
