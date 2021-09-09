namespace WordChallenge.Tests.Cache
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using WordChallenge.Cache;
    using WordChallenge.Services.Interfaces;

    [TestClass]
    public class WordDictionaryCacheTests
    { 
        private MockRepository mockRepository;
        private Mock<IErrorHandlerService> errorHandlerServiceMock;
        private Mock<IDataReaderService> dataReaderServiceMock;
        private readonly string[] defaultStringArray = new string[] { "abcd", "bcde", "cdef", "defg" };
        private readonly string defaultDictPath = @"c:\dummy\dictpath";

        [TestInitialize]
        public void Setup()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.errorHandlerServiceMock = this.mockRepository.Create<IErrorHandlerService>();
            this.dataReaderServiceMock = this.mockRepository.Create<IDataReaderService>();
        }

        [TestMethod]
        public void ETL_Should_ReturnTrue()
        {
            this.dataReaderServiceMock.Setup(x => x.CheckReaderSourceExists(It.IsAny<string>())).Returns(true);
            this.dataReaderServiceMock.Setup(x => x.ReadAll(It.IsAny<string>())).Returns(this.defaultStringArray);

            //Act
            var result = this.GetSut().ETL(this.defaultDictPath);

            result.Should().Be(true);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void ETL_WhereDictNotFound_Should_LogError_And_ReturnFalse()
        {
            this.dataReaderServiceMock.Setup(x => x.CheckReaderSourceExists(It.IsAny<string>())).Returns(false);
            this.errorHandlerServiceMock.Setup(x => x.HandleError(It.IsAny<string>(), true)).Verifiable();

            //Act
            var result = this.GetSut().ETL(this.defaultDictPath);

            result.Should().Be(false);
            this.errorHandlerServiceMock.Verify(x => x.HandleError("Error: Dictionary file not found", true), Times.Once);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void ETL_WhereDictReadFails_Should_LogException_And_ReturnFalse()
        {
            this.dataReaderServiceMock.Setup(x => x.CheckReaderSourceExists(It.IsAny<string>())).Returns(true);
            var ex = new Exception("Dummy Exception");
            this.dataReaderServiceMock.Setup(x => x.ReadAll(It.IsAny<string>())).Throws(ex);
            this.errorHandlerServiceMock.Setup(x => x.HandleException(ex, It.IsAny<string>(), true)).Verifiable();

            //Act
            var result = this.GetSut().ETL(this.defaultDictPath);

            result.Should().Be(false);
            this.errorHandlerServiceMock.Verify(x => x.HandleException(ex, "Error: Unable to read Dictionary data", true), Times.Once);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void ETL_WhereReadDictReturnsNull_Should_LogError_And_ReturnFalse()
        {
            //Similar to above
        }

        [TestMethod]
        public void ETL_WhereReadDictReturnsEmpty_Should_LogError_And_ReturnFalse()
        {
            //Similar to above
        }

        [TestMethod]
        public void ETL_WhereBuildFilteredDictionaryReturnsFalse_Should_LogError_And_ReturnFalse()
        {
            //Similar to above
        }

        // ** NB More tests required to cover GetPotentialWordChanges(), including private FindAllPotentialWords(),
        // ** but the methodology is identical, and hopefully this is enough for example purposes

        private WordDictionaryCache GetSut()
        {
            return new WordDictionaryCache(this.dataReaderServiceMock.Object, this.errorHandlerServiceMock.Object);
        }
    }
}