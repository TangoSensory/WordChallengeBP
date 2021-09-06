using Microsoft.Extensions.DependencyInjection;

namespace WordChallenge.Globals
{
    public static class AppParams
    {
        public static IServiceCollection ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            // We'll come back here later to set up an entry point and
            // our services

            return serviceCollection;
        }
    }
}
