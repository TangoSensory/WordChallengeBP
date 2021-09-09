namespace WordChallenge.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using WordChallenge.Cache.Interfaces;
    using WordChallenge.Model;
    using WordChallenge.Services;
    using WordChallenge.Services.Interfaces;

    [TestClass]
    public class ChallengeSolverTests
    {
        private MockRepository mockRepository;
        private Mock<IWordDictionaryCache> wordCacheMock;
        private Mock<IErrorHandlerService> errorHandlerServiceMock;
        private readonly string[] defaultStringArray = new string[] { "abcd", "bcde", "cdef", "defg" };
        private readonly string defaultStartWord = "save";
        private readonly string defaultTargettWord = "cosh";
        private readonly List<string> emptyWordList = new List<string>();

        private readonly List<string> startWordListLevel1 = new List<string> 
        { 
            "same", // expected choice 
            "rave",
            "savy",
        };
        private readonly List<string> startWordListLevel2 = new List<string>
        {
            "dame",
            "came", // expected choice 
            "some",
        };
        private readonly List<string> startWordListLevel3 = new List<string>
        {
            "dame",
            "come",
            "case", // expected choice 
        };

        private readonly List<string> targetWordListLevel1 = new List<string>
        {
            "gosh",
            "cost", // expected choice 
            "mosh",
        };
        private readonly List<string> targetWordListLevel2 = new List<string>
        {
            "cast", // expected choice 
            "most",
            "host",
        };
        private readonly List<string> targetWordListLevel3 = new List<string>
        {
            "cash",
            "last",
            "case", // expected choice 
        };

        [TestInitialize]
        public void Setup()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.wordCacheMock = this.mockRepository.Create<IWordDictionaryCache>();
            this.errorHandlerServiceMock = this.mockRepository.Create<IErrorHandlerService>();
        }

        [TestMethod]
        public void Solve_Should_ReturnWordCollection()
        {
            var expectedResult = new List<string> { this.defaultStartWord, "same", "came", "case", "cast", "cost", this.defaultTargettWord };
            var startWord = this.defaultStartWord;
            var targetWord = this.defaultTargettWord;
            var wp = new WordPair(startWord, targetWord);
            this.wordCacheMock.SetupGet(x => x.IsDataLoadComplete).Returns(true);
            this.wordCacheMock.Setup(x => x.GetPotentialWordChanges(It.IsAny<string>(), It.IsAny<List<int>>(), It.IsAny<List<string>>(), -1))
                .Returns(this.emptyWordList);

            this.wordCacheMock.Setup(x => x.GetPotentialWordChanges(startWord, wp.MatchedCharacterLocations, emptyWordList, -1))
                .Returns(this.startWordListLevel1);
            this.wordCacheMock.Setup(x => x.GetPotentialWordChanges(targetWord, wp.MatchedCharacterLocations, emptyWordList, -1))
                .Returns(this.targetWordListLevel1);

            startWord = "same";
            targetWord = "cost";
            wp = new WordPair(startWord, targetWord);
            this.wordCacheMock.Setup(x => x.GetPotentialWordChanges(startWord, wp.MatchedCharacterLocations, wp.PreviousStartWords, -1))
                .Returns(this.startWordListLevel2);
            this.wordCacheMock.Setup(x => x.GetPotentialWordChanges(targetWord, wp.MatchedCharacterLocations, wp.PreviousTargetWords, -1))
                .Returns(this.targetWordListLevel2);


            startWord = "came";
            targetWord = "cast";
            wp = new WordPair(startWord, targetWord);
            this.wordCacheMock.Setup(x => x.GetPotentialWordChanges(startWord, wp.MatchedCharacterLocations, wp.PreviousStartWords, -1))
                .Returns(this.startWordListLevel3);
            this.wordCacheMock.Setup(x => x.GetPotentialWordChanges(targetWord, wp.MatchedCharacterLocations, wp.PreviousTargetWords, -1))
                .Returns(this.targetWordListLevel3);

            //Act
            var result = this.GetSut().Solve(this.defaultStartWord, this.defaultTargettWord);

            result.Should().NotBeNull();
            var output = result.ReturnWordChangeHistory().ToList();
            output.Should().NotBeNullOrEmpty();
            output.Count.Should().Be(expectedResult.Count);
            output.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod]
        public void Solve_WithInvalidParams_Should_LogException_And_ReturnNull()
        {
            // NB Because the params are already validated, this scenario should never happen
            var startWord = this.defaultStartWord;
            var targetWord = this.defaultStartWord + "X";
            this.wordCacheMock.SetupGet(x => x.IsDataLoadComplete).Returns(true);
            this.errorHandlerServiceMock.Setup(x => x.HandleException(It.IsAny<Exception>(), It.IsAny<string>(), true)).Verifiable();

            //Act
            var result = this.GetSut().Solve(startWord, targetWord);
            result.Should().BeNull();
            this.errorHandlerServiceMock
                .Verify(x => x.HandleException(It.IsAny<Exception>(), $"Word lengths don't match: {startWord}, {targetWord}", true), Times.Once);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void Solve_WithIncompleteDataLoad_Should_ReturnNull()
        {
            var startWord = this.defaultStartWord;
            var targetWord = this.defaultTargettWord;
            this.wordCacheMock.SetupGet(x => x.IsDataLoadComplete).Returns(false);
            this.errorHandlerServiceMock.Setup(x => x.HandleError(It.IsAny<string>(), true)).Verifiable();

            //Act
            var result = this.GetSut().Solve(this.defaultStartWord, this.defaultTargettWord);

            result.Should().BeNull();
            this.errorHandlerServiceMock.Verify(x => x.HandleError("Error: Dictionary not loaded", true), Times.Once);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void Solve_WhenUnsolveable_Should_LogError_And_ReturnNull()
        {
            var startWord = "wxyz";
            var targetWord = this.defaultTargettWord;
            var wp = new WordPair(startWord, targetWord);
            this.wordCacheMock.SetupGet(x => x.IsDataLoadComplete).Returns(true);
            this.wordCacheMock.Setup(x => x.GetPotentialWordChanges(It.IsAny<string>(), It.IsAny<List<int>>(), It.IsAny<List<string>>(), -1))
                .Returns(this.emptyWordList);

            this.wordCacheMock.Setup(x => x.GetPotentialWordChanges(startWord, wp.MatchedCharacterLocations, emptyWordList, -1))
                .Returns(this.startWordListLevel1);
            this.wordCacheMock.Setup(x => x.GetPotentialWordChanges(targetWord, wp.MatchedCharacterLocations, emptyWordList, -1))
                .Returns(this.targetWordListLevel1);
            this.errorHandlerServiceMock.Setup(x => x.HandleError(It.IsAny<string>(), true)).Verifiable();

            //Act
            var result = this.GetSut().Solve(startWord, this.defaultTargettWord);

            result.Should().BeNull();
            this.errorHandlerServiceMock.Verify(x => x.HandleError("No solution exists for the given words", true), Times.Once);
            this.mockRepository.VerifyAll();
        }

        // ** NB More tests required to cover all code paths in PopulateWordPairs(),
        // ** but the methodology is identical, and hopefully this is enough for example purposes

        private ChallengeSolver GetSut()
        {
            return new ChallengeSolver(this.wordCacheMock.Object, this.errorHandlerServiceMock.Object);
        }
    }
}
