using NUnit.Framework;
using Wall01;

namespace Tests
{
    [TestFixture]
    public class WallTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Read_MessagePosted_MessageTextReturned()
        {
            var subject = new Wall();

            var text = "I love the weather today";
            subject.Post("Alice", text);
            var actual = subject.Read("Alice").Text;

            Assert.AreEqual(text, actual);

        }
    }
}