using System;

namespace Hangman.Visualizing
{
    public interface IVisualizationProvider
    {
        char AcceptGuess();
        void PrintCurrentState(string guess = null);
        void AddVictimPart();
        void GameOver(bool isWon);
        bool RequestIsContinue();
    }
}
