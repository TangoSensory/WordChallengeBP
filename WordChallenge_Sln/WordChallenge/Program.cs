namespace WordChallenge
{
    using Microsoft.Extensions.DependencyInjection;
    using WordChallenge.IoC;

    public class Program
    {
        public static void Main(string[] args)
        {
            var services = ServiceModule.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<EntryPoint>().Execute(args);
        }
    }
}
