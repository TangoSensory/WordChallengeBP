namespace WordChallenge.Tests.Validators
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WordChallenge.Validators;

    // Auto-generated Test Class (Sentry One), and no attempt has been made to fix/populate the tests...
    // Full test coverage is not in the scope of this challenge (and neither, therefore, is TDD)
    // See ChallengeSolverTests and WordDictionaryCacheTests for fully coded examples.

    [TestClass]
    public class StubParamsValidatorTests
    {
        private ParamsValidator _testClass;

        [TestInitialize]
        public void SetUp()
        {
            _testClass = new ParamsValidator();
        }

        [TestMethod]
        public void CanConstruct()
        {
            var instance = new ParamsValidator();
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void CanCallValidateFilenameFormat()
        {
            var path = "TestValue452675417";
            _testClass.ValidateFilenameFormat(path);
            Assert.Fail("Stub test. Completion descoped");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallValidateFilenameFormatWithInvalidPath(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testClass.ValidateFilenameFormat(value));
        }

        [TestMethod]
        public void CanCallValidateWordParams()
        {
            var start = "TestValue136885474";
            var target = "TestValue1319740873";
            _testClass.ValidateWordParams(start, target);
            Assert.Fail("Stub test. Completion descoped");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallValidateWordParamsWithInvalidStart(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testClass.ValidateWordParams(value, "TestValue1464885369"));
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallValidateWordParamsWithInvalidTarget(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testClass.ValidateWordParams("TestValue852049192", value));
        }
    }
}