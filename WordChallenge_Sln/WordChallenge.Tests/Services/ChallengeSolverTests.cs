namespace WordChallenge.Tests.Services
{
    using System;
using System.Collections;
    using System.Collections.Generic;
    using System.Text;
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
        private Mock<IDataReaderService> dataReaderServiceMock;
        private string defaultBlob = @"abcd\nbcde\ncdef\ndefg";
        private string defaultDictPath = @"c:\dummy\dictpath";
        private string defaultOutFilePath = @"c:\dummy\outfilepath";
        private string defaultStartWord = "save";
        private string defaultTargettWord = "cosh";
        private List<string> emptyWordList = new List<string>();
        private List<int> emptyIntList = new List<int>();

        private List<string> startWordListLevel1 = new List<string> 
        { 
            "same",
            "rave",
            "savy",
        };
        private List<string> startWordListLevel2 = new List<string>
        {
            "dame",
            "came",
            "some",
        };
        private List<string> startWordListLevel3 = new List<string>
        {
            "dame",
            "come",
            "case",
        };

        private List<string> targetWordListLevel1 = new List<string>
        {
            "gosh",
            "cost",
            "mosh",
        };
        private List<string> targetWordListLevel2 = new List<string>
        {
            "cast",
            "most",
            "host",
        };
        private List<string> targetWordListLevel3 = new List<string>
        {
            "last",
            "case",
            "cash",
        };

        [TestInitialize]
        public void Setup()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.wordCacheMock = this.mockRepository.Create<IWordDictionaryCache>();
            this.errorHandlerServiceMock = this.mockRepository.Create<IErrorHandlerService>();
            this.dataReaderServiceMock = this.mockRepository.Create<IDataReaderService>();
        }

        [TestMethod]
        public void Initialise_ShouldReturn_True()
        {
            this.wordCacheMock.SetupGet(x => x.IsDataLoadComplete).Returns(true);
            this.wordCacheMock.Setup(x => x.LoadFilteredDictionary(It.IsAny<string>(), -1)).Returns(true);
            this.dataReaderServiceMock.Setup(x => x.CheckReaderSourceExists(It.IsAny<string>())).Returns(true);
            this.dataReaderServiceMock.Setup(x => x.ReadAll(It.IsAny<string>())).Returns(this.defaultBlob);

            bool result = false;
            result = this.GetSut().Initialise(this.defaultDictPath, this.defaultOutFilePath);

            result.Should().Be(true);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void Initialise_WhereDictNotFound_ShouldReturn_False()
        {
            this.dataReaderServiceMock.Setup(x => x.CheckReaderSourceExists(It.IsAny<string>())).Returns(false);
            this.errorHandlerServiceMock.Setup(x => x.HandleError(It.IsAny<string>(), true)).Verifiable();

            bool result = false;
            result = this.GetSut().Initialise(this.defaultDictPath, this.defaultOutFilePath);

            result.Should().Be(false);
            this.errorHandlerServiceMock.Verify(x => x.HandleError("Error: Dictionary file not found", true), Times.Once);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void Initialise_WhereDictReadFails_ShouldReturn_False()
        {
            this.dataReaderServiceMock.Setup(x => x.CheckReaderSourceExists(It.IsAny<string>())).Returns(true);
            var ex = new Exception("Dummy Exception");
            this.dataReaderServiceMock.Setup(x => x.ReadAll(It.IsAny<string>())).Throws(ex);

            bool result = false;
            result = this.GetSut().Initialise(this.defaultDictPath, this.defaultOutFilePath);

            result.Should().Be(true);
            this.errorHandlerServiceMock.Verify(x => x.HandleException(ex, "Error: Unable to read Dictionary data", true), Times.Once);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void Initialise_WhereReadDictReturnsNull_ShouldReturn_False()
        {
            //Similar to above
        }

        [TestMethod]
        public void Initialise_WhereReadDictReturnsEmpty_ShouldReturn_False()
        {
            //Similar to above
        }

        [TestMethod]
        public void Initialise_WhereLoadDictReturnsFalse_ShouldReturn_False()
        {
            //Similar to above
        }

        [TestMethod]
        public void Solve_ShouldReturn_WordCollection()
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

            var result = this.GetSut().Solve(this.defaultStartWord, this.defaultTargettWord);

            result.Should().NotBeNullOrEmpty();
            result.Count.Should().Be(expectedResult.Count);
            result.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod]
        public void Solve_WIthIncompleteDataLoad_ShouldReturn_Null()
        {

        }

        private ChallengeSolver GetSut()
        {
            return new ChallengeSolver(this.wordCacheMock.Object, this.errorHandlerServiceMock.Object, this.dataReaderServiceMock.Object);
        }
    }
}
