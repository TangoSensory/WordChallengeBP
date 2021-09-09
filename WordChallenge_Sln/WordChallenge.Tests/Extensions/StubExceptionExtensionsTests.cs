namespace WordChallenge.Tests.Extensions
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WordChallenge.Extensions;

    // Auto-generated Test Class (Sentry One), and no attempt has been made to fix/populate the tests...
    // Full test coverage is not in the scope of this challenge (and neither, therefore, is TDD)
    // See ChallengeSolverTests and WordDictionaryCacheTests for fully coded examples.

    [TestClass]
  public class StubExceptionExtensionsTests
  {
    [TestMethod]
    public void CanCallFullString()
    {
      var exc = new Exception();
      var result = exc.FullString();
      Assert.Fail("Stub test. Completion Create or modify test");
    }

    [TestMethod]
    public void CannotCallFullStringWithNullExc()
    {
      Assert.ThrowsException<ArgumentNullException>(() => default(Exception).FullString());
    }
  }
}