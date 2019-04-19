using Moq;
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

            var dateDiffProvider = GetDateDiffProviderForPostFiveMinutesAgo();
            var subject = new Wall(dateDiffProvider.Object);

            var text = "I love the weather today";
            subject.Post("Alice", text);
            var actual = subject.Read("Alice").Text;

            Assert.AreEqual(text, actual);
        }

        private static Mock<IDateDiff> GetDateDiffProviderForPostFiveMinutesAgo()
        {
            var minutesAgo = "(5 minutes ago)";
            var dateDiffProvider = GetMockDateDiffProvider(minutesAgo);
            return dateDiffProvider;
        }

        private static Mock<IDateDiff> GetMockDateDiffProvider(string minutesAgo)
        {
            var dateDiffProvider = new Mock<IDateDiff>();
            dateDiffProvider.Setup(d => d.GetTimeSincePosted(It.IsAny<Post>())).Returns(minutesAgo);
            return dateDiffProvider;
        }

        [Test]
        public void Read_MessagePosted_TimestampReturned()
        {
            var minutesAgo = "(5 minutes ago)";
            var dateDiffProvider = GetMockDateDiffProvider(minutesAgo);
            var subject = new Wall(dateDiffProvider.Object);

            var text = "I love the weather today";
            subject.Post("Alice", text);
            var actual = subject.Read("Alice").TimeSince;

            Assert.AreEqual(minutesAgo, actual);
        }
    }


}