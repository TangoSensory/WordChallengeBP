namespace WordChallenge.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Extensions.DependencyInjection;

    public class ServiceModule
    {
        public static IServiceCollection ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<EntryPoint>();

            return serviceCollection;
        }
    }
}
