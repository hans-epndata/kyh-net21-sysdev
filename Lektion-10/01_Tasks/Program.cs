using System.Diagnostics;

namespace _01_Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.ReadKey();
        //Console.WriteLine($"Main: {Thread.CurrentThread.ManagedThreadId}");

        //for (int i = 0; i < 5000; i++)
        //{

        //    Task.Run(() =>
        //    {
        //        Console.WriteLine($"Task:{Task.CurrentId} \t Thread: {Thread.CurrentThread.ManagedThreadId}");
        //        Task.Delay(10000);
        //        Console.WriteLine($"Task:{Task.CurrentId} \t Thread: {Thread.CurrentThread.ManagedThreadId} - Completed");

        //    });
        //}

        Console.Clear();
        Console.WriteLine("Din beställning är skickad.");

        OrderAsync();

        Task.Delay(2000);
        Console.WriteLine("Din mat är nu leverarad.");

        Console.ReadKey();
    }


    private static async Task OrderAsync()
    {

        await Task.Delay(2000);
        Console.WriteLine("Din beställning är mottagen.");

        await Task.Delay(2000);
        Console.WriteLine("Din beställning tillagas.");

        await Task.Delay(5000);
        Console.WriteLine("Din beställning är klar för leverans");

    }
}