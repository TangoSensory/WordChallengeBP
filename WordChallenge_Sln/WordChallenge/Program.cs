namespace WordChallenge
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using WordChallenge.IoC;

    public class Program
    {
        public static void Main(string[] args)
        {
            var services = ServiceModule.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<EntryPoint>().Execute(args);

            // NB In a production system, there'd be another Interface here to abstract the actual output device and
            // enable full testing of this method
            Console.WriteLine($"PRESS ANY KEY TO EXIT");
            Console.ReadKey();
        }
    }
}
