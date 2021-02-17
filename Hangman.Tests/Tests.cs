using Hangman.Core;
using Hangman.Visualizing;
using HangmanTest.WordsProvider.Services;
using Moq;
using System;
using Xunit;

namespace Hangman.Tests
{
    public class Tests
    {
        private Mock<IVisualizationProvider> _visualizationProviderMock = new Mock<IVisualizationProvider>();
        private Mock<IWordsProvider> _wordsProviderMock = new Mock<IWordsProvider>();

        [Fact]
        public void GivenWordGenerated_WhenIGuessAllCharsWithoutMistakes_ThenIWin()
        {
            _wordsProviderMock.Setup(x => x.GenerateWord())
                .Returns("apple");

            _visualizationProviderMock.SetupSequence(x => x.AcceptGuess())
                .Returns('a')
                .Returns('p')
                .Returns('l')
                .Returns('e');

            _visualizationProviderMock.Setup(x => x.RequestIsContinue())
                .Returns(false);

            var game = new HangmanGame(_wordsProviderMock.Object, _visualizationProviderMock.Object);
            game.Run();

            _visualizationProviderMock.Verify(x => x.AddVictimPart(), Times.Never);
            _visualizationProviderMock.Verify(x => x.GameOver(It.Is<bool>(x => x == false)), Times.Never);
            _visualizationProviderMock.Verify(x => x.GameOver(It.Is<bool>(x => x == true)), Times.Once);
        }

        [Fact]
        public void GivenWordGenerated_WhenIGuessAllCharsWithOneMistake_ThenIWin()
        {
            MockAppleWord();

            _visualizationProviderMock.SetupSequence(x => x.AcceptGuess())
                .Returns('a')
                .Returns('p')
                .Returns('n')
                .Returns('l')
                .Returns('e');

            _visualizationProviderMock.Setup(x => x.RequestIsContinue())
                .Returns(false);

            var game = new HangmanGame(_wordsProviderMock.Object, _visualizationProviderMock.Object);
            game.Run();

            _visualizationProviderMock.Verify(x => x.AddVictimPart(), Times.Once);
            _visualizationProviderMock.Verify(x => x.GameOver(It.Is<bool>(x => x == false)), Times.Never);
            _visualizationProviderMock.Verify(x => x.GameOver(It.Is<bool>(x => x == true)), Times.Once);
        }

        [Fact]
        public void GivenWordGenerated_WhenIMakeSevenMistakes_ThenILose()
        {
            MockAppleWord();

            _visualizationProviderMock.SetupSequence(x => x.AcceptGuess())
                .Returns('a')
                .Returns('p')
                .Returns('n')
                .Returns('s')
                .Returns('d')
                .Returns('f')
                .Returns('z')
                .Returns('q')
                .Returns('y')
                ;

            _visualizationProviderMock.Setup(x => x.RequestIsContinue())
                .Returns(false);

            var game = new HangmanGame(_wordsProviderMock.Object, _visualizationProviderMock.Object);
            game.Run();

            _visualizationProviderMock.Verify(x => x.AddVictimPart(), Times.Exactly(7));
            _visualizationProviderMock.Verify(x => x.GameOver(It.Is<bool>(x => x == false)), Times.Once);
            _visualizationProviderMock.Verify(x => x.GameOver(It.Is<bool>(x => x == true)), Times.Never);
        }

        [Fact]
        public void GivenWordGenerated_WhenIMakeSevenMistakes_AndWillTryAgain_ThenILoseAndWin()
        {
            MockAppleWord();

            _visualizationProviderMock.SetupSequence(x => x.AcceptGuess())
                .Returns('a')
                .Returns('p')
                .Returns('n')
                .Returns('s')
                .Returns('d')
                .Returns('f')
                .Returns('z')
                .Returns('q')
                .Returns('y')
                .Returns('a')
                .Returns('p')
                .Returns('l')
                .Returns('e');

            _visualizationProviderMock.SetupSequence(x => x.RequestIsContinue())
                .Returns(true)
                .Returns(false);

            var game = new HangmanGame(_wordsProviderMock.Object, _visualizationProviderMock.Object);
            game.Run();

            _visualizationProviderMock.Verify(x => x.AddVictimPart(), Times.Exactly(7));
            _visualizationProviderMock.Verify(x => x.GameOver(It.Is<bool>(x => x == false)), Times.Once);
            _visualizationProviderMock.Verify(x => x.GameOver(It.Is<bool>(x => x == true)), Times.Once);
        }


        private void MockAppleWord()
        {
            _wordsProviderMock.Setup(x => x.GenerateWord())
                .Returns("apple");
        }
    }
}
