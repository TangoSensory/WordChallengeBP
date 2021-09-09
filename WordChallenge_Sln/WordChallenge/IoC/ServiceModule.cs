namespace WordChallenge.IoC
{
    using Microsoft.Extensions.DependencyInjection;
    using WordChallenge.Cache;
    using WordChallenge.Cache.Interfaces;
    using WordChallenge.Services;
    using WordChallenge.Services.Interfaces;
    using WordChallenge.Validators;
    using WordChallenge.Validators.Interfaces;

    public class ServiceModule
    {
        public static IServiceCollection ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<EntryPoint>();

            serviceCollection.AddSingleton<IWordDictionaryCache, WordDictionaryCache>();

            serviceCollection.AddTransient<IChallengeSolver, ChallengeSolver>();
            serviceCollection.AddTransient<IDataReaderService, FileReaderService>();
            serviceCollection.AddTransient<IDataWriterService, FileWriterService>();
            serviceCollection.AddTransient<IErrorHandlerService, ErrorHandlerService>();
            serviceCollection.AddTransient<IParamsValidator, ParamsValidator>();

            return serviceCollection;
        }
    }
}
