namespace WordChallenge.Tests.Helpers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WordChallenge.Helpers;

    // Auto-generated Test Class (Sentry One), and no attempt has been made to fix/populate the tests...
    // Full test coverage is not in the scope of this challenge (and neither, therefore, is TDD)
    // See ChallengeSolverTests and WordDictionaryCacheTests for fully coded examples.

    [TestClass]
    public class StubWordHelperTests
    {
        [TestMethod]
        public void CanCallGetUnmatchedCharacters()
        {
            var word1 = "TestValue1407939318";
            var word2 = "TestValue244173423";
            var result = WordHelper.GetUnmatchedCharacters(word1, word2);
            Assert.Fail("Stub test. Completion descoped");
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallGetUnmatchedCharactersWithInvalidWord1(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => WordHelper.GetUnmatchedCharacters(value, "TestValue2137573447"));
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallGetUnmatchedCharactersWithInvalidWord2(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => WordHelper.GetUnmatchedCharacters("TestValue501381733", value));
        }
    }
}