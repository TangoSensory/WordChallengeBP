namespace WordChallenge.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using WordChallenge;
    using WordChallenge.Validators.Interfaces;
    using WordChallenge.Services.Interfaces;
    using WordChallenge.Cache.Interfaces;

    // Auto-generated Test Class (Sentry One), and no attempt has been made to fix/populate the tests...
    // Full test coverage is not in the scope of this challenge (and neither, therefore, is TDD)
    // See ChallengeSolverTests and WordDictionaryCacheTests for fully coded examples.

    [TestClass]
    public class StubEntryPointTests
    {
        private EntryPoint _testClass;

        [TestInitialize]
        public void SetUp()
        {
            _testClass = new EntryPoint(new Mock<IParamsValidator>().Object, new Mock<IChallengeSolver>().Object, new Mock<IErrorHandlerService>().Object, new Mock<IDataReaderService>().Object, new Mock<IDataWriterService>().Object, new Mock<IWordDictionaryCache>().Object);
        }

        [TestMethod]
        public void CanCallExecute()
        {
            var args = new[] { "TestValue1785273869", "TestValue4290555", "TestValue658161089" };
            _testClass.Execute(args);
            Assert.Fail("Stub test. Completion descoped");
        }

        [TestMethod]
        public void CannotCallExecuteWithNullArgs()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testClass.Execute(default(string[])));
        }
    }
}