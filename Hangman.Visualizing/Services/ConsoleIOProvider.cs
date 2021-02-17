using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Hangman.Visualizing
{
    public class ConsoleIOProvider : IVisualizationProvider
    {
        private List<string> CurrentState;
        private Stack<string> Victim;

        public ConsoleIOProvider()
        {
            InitializeVictim();
        }

        public char AcceptGuess()
        {
            Console.WriteLine("Please enter your guess: ");
            var input = Console.ReadLine();
            var singleLeterRegex = new Regex("[a-zA-Z]");
            while (!singleLeterRegex.IsMatch(input) || input.Length > 1)
            {
                Console.WriteLine("Only one alphabetical symbol is allowed.");
                input = Console.ReadLine();
            }
            return char.Parse(input);
        }

        private void InitializeVictim()
        {
            var list = new List<string> {
            " | ",
            " | / \\ ",
            " |  | ",
            " | /|\\ ",
            " | ( ) ",
            " |  | ",
            " |_______ "
            };

            Victim = new Stack<string>(list);
            CurrentState = new List<string>();
        }

        public void PrintCurrentState(string guess = null)
        {
            Console.Clear();
            if (!string.IsNullOrEmpty(guess))
            {
                Console.WriteLine(guess);
            }
            CurrentState.ForEach(i => Console.WriteLine(i));
        }

        public void AddVictimPart()
        {
            if (Victim.Count == 0)
            {
                InitializeVictim();
            }
            var part = Victim.Pop();
            CurrentState.Add(part);
            PrintCurrentState();
        }

        public void GameOver(bool isWon)
        {
            Console.Clear();
            if (isWon)
            {
                Console.WriteLine("You win!");
            }
            else
            {
                PrintCurrentState();
                Console.WriteLine("You lose :(");
            }

            Console.WriteLine("Game over.");
            Console.ReadLine();
        }

        public bool RequestIsContinue()
        {
            Console.WriteLine("Continue? y/n");
            var answer = Console.ReadLine().ToLower();
            switch (answer)
            {
                case "y":
                    InitializeVictim();
                    return true;
                case "n":
                default:
                    return false;
            }
        }
    }
}
