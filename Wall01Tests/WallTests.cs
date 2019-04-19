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


        [Test]
        public void Read_1MessagePostedByAliceAnd2ByBob_PostsReturned()
        {
            var alicePost1MinutesAgo = "(5 minutes ago)";
            var alicePost1Text = "I love the weather today";
            var bobPost1MinutesAgo = "(2 minutes ago)";
            var bobPost1Text = "Damn! We lost!";
            var bobPost2MinutesAgo = "(5 minutes ago)";
            var bobPost2Text = "Good game though.";

            var dateDiffProvider = new Mock<IDateDiff>();
            dateDiffProvider.Setup(d => d.GetTimeSincePosted(It.Is<Post>(p => p.Text == alicePost1Text))).Returns(alicePost1MinutesAgo);
            dateDiffProvider.Setup(d => d.GetTimeSincePosted(It.Is<Post>(p => p.Text == bobPost1Text))).Returns(bobPost1MinutesAgo);
            dateDiffProvider.Setup(d => d.GetTimeSincePosted(It.Is<Post>(p => p.Text == bobPost2Text))).Returns(bobPost2MinutesAgo);


            var subject = new Wall(dateDiffProvider.Object);

            
            subject.Post("Alice", alicePost1Text);
            subject.Post("Bob", bobPost1Text);
            subject.Post("Bob", bobPost2Text);

            var minsAgo = alicePost1MinutesAgo;
            var text = alicePost1Text;
            var userName = "Alice";
            AssertPostReturnedByWall(subject, userName, minsAgo, text);
            AssertPostReturnedByWall(subject, "Bob", bobPost1MinutesAgo, bobPost1Text);
            //AssertPostReturnedByWall(subject, "Bob", bobPost2MinutesAgo, bobPost2Text);
        }

        private static void AssertPostReturnedByWall(Wall subject, string userName, string minsAgo, string text)
        {
            var actual = subject.Read(userName);
            Assert.AreEqual(minsAgo, actual.TimeSince);
            Assert.AreEqual(text, actual.Text);
        }
    }


}