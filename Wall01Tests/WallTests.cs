using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Wall01;

namespace Tests
{
    [TestFixture]
    public class WallTests
    {
        string _alicePost1MinutesAgo = "(5 minutes ago)";
        string _alicePost1Text = "I love the weather today";
        string _bobPost1MinutesAgo = "(2 minutes ago)";
        string _bobPost1Text = "Damn! We lost!";
        string _bobPost2MinutesAgo = "(1 minute ago)";
        string _bobPost2Text = "Good game though.";
        string _charliePost1MinsAgo = "(2 seconds ago)";
        string _charliePost1Text = "I'm in New York today! Anyone wants to have a coffee?";


        private static string Alice => "Alice";
        private static string Bob => "Bob";
        private static string Charlie => "Charlie";


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
            var actual = subject.Read("Alice").First().Text;

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
            var actual = subject.Read("Alice").First().TimeSince;

            Assert.AreEqual(minutesAgo, actual);
        }


        [Test]
        public void Read_1MessagePostedByAliceAnd2ByBob_PostsReturned()
        {
            var subject = GetSubjectWithDateDiffProvider();

            subject.Post("Alice", _alicePost1Text);
            subject.Post("Bob", _bobPost1Text);
            subject.Post("Bob", _bobPost2Text);

            var minsAgo = _alicePost1MinutesAgo;
            var text = _alicePost1Text;
            var userName = "Alice";
            var actual = subject.Read(userName);
            Assert.AreEqual(1, actual.Count);
            AssertPostReturnedByWallRead(minsAgo, text, actual.First());

            actual = subject.Read("Bob");
            Assert.AreEqual(2, actual.Count);

            AssertPostReturnedByWallRead(_bobPost2MinutesAgo, _bobPost2Text, actual.First());
            AssertPostReturnedByWallRead(_bobPost1MinutesAgo, _bobPost1Text, actual.Last());
        }

        private Wall GetSubjectWithDateDiffProvider()
        {
            var dateDiffProvider = new Mock<IDateDiff>();
            dateDiffProvider.Setup(d => d.GetTimeSincePosted(It.Is<Post>(p => p.Text == _alicePost1Text)))
                .Returns(_alicePost1MinutesAgo);
            dateDiffProvider.Setup(d => d.GetTimeSincePosted(It.Is<Post>(p => p.Text == _bobPost1Text)))
                .Returns(_bobPost1MinutesAgo);
            dateDiffProvider.Setup(d => d.GetTimeSincePosted(It.Is<Post>(p => p.Text == _bobPost2Text)))
                .Returns(_bobPost2MinutesAgo);
            dateDiffProvider.Setup(d => d.GetTimeSincePosted(It.Is<Post>(p => p.Text == _charliePost1Text)))
                .Returns(_charliePost1MinsAgo);
            var subject = new Wall(dateDiffProvider.Object);
            return subject;
        }

        private static void AssertPostReturnedByWallRead(string minsAgo, string text, HistoricPost actual)
        {
            Assert.AreEqual(minsAgo, actual.TimeSince);
            Assert.AreEqual(text, actual.Text);
        }


        [Test]
        public void Wall_1MessagePostedByCharlie_PostReturned()
        {

            var subject = GetSubjectWithDateDiffProvider();

            var userName = "Charlie";
            subject.Post(userName, _charliePost1Text);

            var expected = new List<string>
                {"Charlie - I'm in New York today! Anyone wants to have a coffee? (2 seconds ago)"};
            var responses = subject.GetWall(userName);
            var actual = responses.Select(r => r.FormattedOutputPrependedWithUserName).ToList();
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            
        }

        [Test]
        public void Wall_2MessagesPostedByBob_PostsReturned()
        {
            var subject = GetSubjectWithDateDiffProvider();

            var userName = "Bob";
            subject.Post("Bob", _bobPost1Text);
            subject.Post("Bob", _bobPost2Text);


            var expected = new List<string>
            {
                "Bob - Good game though. (1 minute ago)",
                "Bob - Damn! We lost! (2 minutes ago)"
            };
            var responses = subject.GetWall(userName);
            var actual = responses.Select(r => r.FormattedOutputPrependedWithUserName).ToList();
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }

        }

        [Test]
        public void Follow_CharlieFollowsAliceAndThenBob_CharlieBobAndAlicePostsReturnedInRecencyOrder()
        {
            var subject = GetSubjectWithDateDiffProvider();

            subject.Post(Alice, _alicePost1Text);
            subject.Post(Bob, _bobPost1Text);
            subject.Post(Bob, _bobPost2Text);
            subject.Post(Charlie, _charliePost1Text);

            subject.Follow(Charlie, Alice);
            var expected = new List<string>
            {
                "Charlie - I'm in New York today! Anyone wants to have a coffee? (2 seconds ago)",
                "Alice - I love the weather today (5 minutes ago)"
            };
            var responses = subject.GetWall(Charlie);
            var actual = responses.Select(r => r.FormattedOutputPrependedWithUserName).ToList();
            AssertGetWallResponsesEqualExpected(expected, actual);

            subject.Follow(Charlie, Bob);
            expected = new List<string>
            {
                "Charlie - I'm in New York today! Anyone wants to have a coffee? (2 seconds ago)",
                "Bob - Good game though. (1 minute ago)",
                "Bob - Damn! We lost! (2 minutes ago)",
                "Alice - I love the weather today (5 minutes ago)"
            };
            responses = subject.GetWall(Charlie);
            actual = responses.Select(r => r.FormattedOutputPrependedWithUserName).ToList();
            AssertGetWallResponsesEqualExpected(expected, actual);

        }

        private static void AssertGetWallResponsesEqualExpected(List<string> expected, List<string> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [Test]
        public void Follow_AliceFollowsCharlieAndThenBob_CharlieBobAndAlicePostsReturnedInRecencyOrder()
        {
            var subject = GetSubjectWithDateDiffProvider();

            subject.Post(Alice, _alicePost1Text);
            subject.Post(Bob, _bobPost1Text);
            subject.Post(Bob, _bobPost2Text);
            subject.Post(Charlie, _charliePost1Text);

            subject.Follow( Alice, Charlie);
            var expected = new List<string>
            {
                "Charlie - I'm in New York today! Anyone wants to have a coffee? (2 seconds ago)",
                "Alice - I love the weather today (5 minutes ago)"
            };
            var responses = subject.GetWall(Alice);
            var actual = responses.Select(r => r.FormattedOutputPrependedWithUserName).ToList();
            AssertGetWallResponsesEqualExpected(expected, actual);

            subject.Follow(Alice, Bob);
            expected = new List<string>
            {
                "Charlie - I'm in New York today! Anyone wants to have a coffee? (2 seconds ago)",
                "Bob - Good game though. (1 minute ago)",
                "Bob - Damn! We lost! (2 minutes ago)",
                "Alice - I love the weather today (5 minutes ago)"
            };
            responses = subject.GetWall(Alice);
            actual = responses.Select(r => r.FormattedOutputPrependedWithUserName).ToList();
            AssertGetWallResponsesEqualExpected(expected, actual);

        }


    }


}