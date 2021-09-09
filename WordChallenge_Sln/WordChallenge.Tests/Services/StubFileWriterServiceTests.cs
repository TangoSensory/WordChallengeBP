namespace WordChallenge.Tests.Services
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using WordChallenge.Services;
    using WordChallenge.Services.Interfaces;

    // Auto-generated Test Class (Sentry One), and no attempt has been made to fix/populate the tests...
    // Full test coverage is not in the scope of this challenge (and neither, therefore, is TDD)
    // See ChallengeSolverTests and WordDictionaryCacheTests for fully coded examples.

    [TestClass]
    public class StubFileWriterServiceTests
    {
        private FileWriterService _testClass;
        private IErrorHandlerService _errorHandlerService;

        [TestInitialize]
        public void SetUp()
        {
            _errorHandlerService = new Mock<IErrorHandlerService>().Object;
            _testClass = new FileWriterService(_errorHandlerService);
        }

        [TestMethod]
        public void CanConstruct()
        {
            var instance = new FileWriterService(_errorHandlerService);
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void CannotConstructWithNullErrorHandlerService()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new FileWriterService(default(IErrorHandlerService)));
        }

        [TestMethod]
        public void CanCallCreateOutptTarget()
        {
            var path = "TestValue1817307059";
            var result = _testClass.CreateOutptTarget(path);
            Assert.Fail("Stub test. Completion descoped");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallCreateOutptTargetWithInvalidPath(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testClass.CreateOutptTarget(value));
        }

        [TestMethod]
        public void CanCallWrite()
        {
            var outputText = "TestValue460117447";
            var result = _testClass.Write(outputText);
            Assert.Fail("Stub test. Completion descoped");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallWriteWithInvalidOutputText(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testClass.Write(value));
        }
    }
}