namespace WordChallenge.Tests.Services
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WordChallenge.Services;

    // Auto-generated Test Class (Sentry One), and no attempt has been made to fix/populate the tests...
    // Full test coverage is not in the scope of this challenge (and neither, therefore, is TDD)
    // See ChallengeSolverTests and WordDictionaryCacheTests for fully coded examples.

    [TestClass]
    public class StubErrorHandlerServiceTests
    {
        private ErrorHandlerService _testClass;

        [TestInitialize]
        public void SetUp()
        {
            _testClass = new ErrorHandlerService();
        }

        [TestMethod]
        public void CanConstruct()
        {
            var instance = new ErrorHandlerService();
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void CanCallHandleError()
        {
            var errorMessage = "TestValue660460241";
            var notifyUser = false;
            _testClass.HandleError(errorMessage, notifyUser);
            Assert.Fail("Stub test. Completion descoped");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallHandleErrorWithInvalidErrorMessage(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testClass.HandleError(value, true));
        }

        [TestMethod]
        public void CanCallHandleException()
        {
            var ex = new Exception();
            var additionalErrorMessage = "TestValue1995111452";
            var notifyUser = true;
            _testClass.HandleException(ex, additionalErrorMessage, notifyUser);
            Assert.Fail("Stub test. Completion descoped");
        }

        [TestMethod]
        public void CannotCallHandleExceptionWithNullEx()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testClass.HandleException(default(Exception), "TestValue210063544", true));
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallHandleExceptionWithInvalidAdditionalErrorMessage(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testClass.HandleException(new Exception(), value, false));
        }
    }
}