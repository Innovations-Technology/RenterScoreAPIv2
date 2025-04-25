using NUnit.Framework;

namespace RenterScoreAPIv2.Test.Unit
{
    [TestFixture]
    public class FailTest
    {
        [Test]
        public void ThisTestWillAlwaysFail()
        {
            Assert.Fail("This test is designed to always fail.");
        }
    }
}