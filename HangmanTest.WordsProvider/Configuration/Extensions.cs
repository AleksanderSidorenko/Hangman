using HangmanTest.WordsProvider.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HangmanTest.WordsProvider.Configuration
{
    public static class Extensions
    {
        public static IServiceCollection AddFileWordsProvider(this IServiceCollection services)
        {
            services.AddSingleton<IWordsProvider, FileWordsProvider>();
            return services;
        }
    }
}
