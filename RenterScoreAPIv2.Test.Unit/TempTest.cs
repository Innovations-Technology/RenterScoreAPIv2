using NUnit.Framework;

namespace RenterScoreAPIv2.Test.Unit
{
    [TestFixture]
    public class TempTest
    {
        [SetUp]
        public void Setup()
        {
            // Code to set up test environment
        }

        [Test]
        public void SampleTest()
        {
            // Arrange
            int expected = 5;

            // Act
            int actual = 2 + 3;

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "2 + 3 should equal 5");
        }

        [TearDown]
        public void TearDown()
        {
            // Code to clean up after tests
        }
    }
}