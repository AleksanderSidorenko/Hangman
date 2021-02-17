using System;
using System.IO;

namespace HangmanTest.WordsProvider.Services
{
    public class FileWordsProvider : IWordsProvider
    {
        private string[] Words;

        public FileWordsProvider()
        {
            Words = File.ReadAllLines("words.txt");
        }

        public string GenerateWord()
        {
            var random = new Random();
            var numberOfRaw = random.Next(0, Words.Length - 1);
            return Words[numberOfRaw];
        }
    }
}
