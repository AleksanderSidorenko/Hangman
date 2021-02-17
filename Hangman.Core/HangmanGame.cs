using Hangman.Visualizing;
using HangmanTest.WordsProvider.Services;
using System;
using System.Linq;

namespace Hangman.Core
{
    public class HangmanGame : IGame
    {
        private readonly IWordsProvider _wordsProvider;

        private readonly IVisualizationProvider _visualizationProvider;

        public HangmanGame(IWordsProvider wordsProvider, IVisualizationProvider visualizationProvider)
        {
            _wordsProvider = wordsProvider;
            _visualizationProvider = visualizationProvider;
        }

        public void Run()
        {
            var isContinue = true;

            while (isContinue)
            {
                Play();

                isContinue = _visualizationProvider.RequestIsContinue();
            }
        }

        public void Play()
        {
            var word = _wordsProvider.GenerateWord();

            var guessLine = Enumerable.Repeat('*', word.Length).ToArray();

            var isWon = false;
            var numOfMistakes = 0;
            while (!IsEndOfGame(numOfMistakes) && !isWon)
            {
                _visualizationProvider.PrintCurrentState(new string(guessLine));
                var playerGuess = _visualizationProvider.AcceptGuess(); ;
                var guessed = false;
                for (int i = 0; i < word.Length; i++)
                {
                    if (playerGuess == word[i])
                    {
                        guessLine[i] = playerGuess;
                        guessed = true;
                    }
                }

                if (!guessed)
                {
                    _visualizationProvider.AddVictimPart();
                    numOfMistakes++;
                }

                if (!guessLine.Contains('*'))
                {
                    isWon = true;
                }
            }

            _visualizationProvider.GameOver(isWon);
        }

        private bool IsEndOfGame(int mistakes)
        {
            return mistakes == 7;
        }
    }
}
