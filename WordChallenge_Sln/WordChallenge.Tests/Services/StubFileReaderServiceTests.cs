namespace WordChallenge.Tests.Services
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WordChallenge.Services;

    // Auto-generated Test Class (Sentry One), and no attempt has been made to fix/populate the tests...
    // Full test coverage is not in the scope of this challenge (and neither, therefore, is TDD)
    // See ChallengeSolverTests and WordDictionaryCacheTests for fully coded examples.

    [TestClass]
    public class StubFileReaderServiceTests
    {
        private FileReaderService _testClass;

        [TestInitialize]
        public void SetUp()
        {
            _testClass = new FileReaderService();
        }

        [TestMethod]
        public void CanConstruct()
        {
            var instance = new FileReaderService();
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void CanCallCheckReaderSourceExists()
        {
            var path = "TestValue1528079079";
            var result = _testClass.CheckReaderSourceExists(path);
            Assert.Fail("Stub test. Completion descoped");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallCheckReaderSourceExistsWithInvalidPath(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testClass.CheckReaderSourceExists(value));
        }

        [TestMethod]
        public void CanCallReadAll()
        {
            var path = "TestValue1267562769";
            var result = _testClass.ReadAll(path);
            Assert.Fail("Stub test. Completion descoped");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallReadAllWithInvalidPath(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testClass.ReadAll(value));
        }
    }
}