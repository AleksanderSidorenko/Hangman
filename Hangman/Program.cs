using Hangman.Core;
using HangmanTest.Visualizing.Configuration;
using HangmanTest.WordsProvider.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddFileWordsProvider();
            services.AddConsoleVisualization();
            services.AddTransient<IGame, HangmanGame>();

            var game = services.BuildServiceProvider().GetRequiredService<IGame>();
            game.Run();
        }
    }
}
