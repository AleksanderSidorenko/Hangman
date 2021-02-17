using Hangman.Visualizing;
using Microsoft.Extensions.DependencyInjection;

namespace HangmanTest.Visualizing.Configuration
{
    public static class Extensions
    {
        public static IServiceCollection AddConsoleVisualization(this IServiceCollection services)
        {
            services.AddScoped<IVisualizationProvider, ConsoleIOProvider>();
            return services;
        }
    }
}
