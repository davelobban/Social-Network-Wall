using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Wall01;
using WallConsole;

namespace Tests
{
    public class InputHandlerTests
    {
        private const string aliceInput1 = "Alice -> I love the weather today";
        private const string bobInput1 = "Bob -> Damn! We lost!";
        private const string bobInput2 = "Bob -> Good game though.";
        private const string charlieInput1 = "Charlie -> I'm in New York today! Anyone wants to have a coffee?";
        readonly string _alicePost1MinutesAgo = "(5 minutes ago)";
        readonly string _alicePost1Text = "I love the weather today";
        readonly string _bobPost1MinutesAgo = "(2 minutes ago)";
        readonly string _bobPost1Text = "Damn! We lost!";
        readonly string _bobPost2MinutesAgo = "(1 minute ago)";
        readonly string _bobPost2Text = "Good game though.";
        readonly string _charliePost1MinsAgo = "(2 seconds ago)";
        readonly string _charliePost1Text = "I'm in New York today! Anyone wants to have a coffee?";


        private IDateDiff GetDateDiffProvider()
        {
            var dateDiffProvider = new Mock<IDateDiff>();
            dateDiffProvider.Setup(d => d.GetTimeSincePosted(It.Is<Post>(p => p.Text .Contains( _alicePost1Text))))
                .Returns(_alicePost1MinutesAgo);                                                  
            dateDiffProvider.Setup(d => d.GetTimeSincePosted(It.Is<Post>(p => p.Text .Contains( _bobPost1Text))))
                .Returns(_bobPost1MinutesAgo);                                                    
            dateDiffProvider.Setup(d => d.GetTimeSincePosted(It.Is<Post>(p => p.Text .Contains( _bobPost2Text))))
                .Returns(_bobPost2MinutesAgo);                                                    
            dateDiffProvider.Setup(d => d.GetTimeSincePosted(It.Is<Post>(p => p.Text .Contains( _charliePost1Text))))
                .Returns(_charliePost1MinsAgo);
            return dateDiffProvider.Object;
        }

        private InputHandler GetSubjectWithMessagesPosted()
        {
            var subject = new InputHandler(GetDateDiffProvider());
            var inputs = new List<string>
            {
                aliceInput1,
                bobInput1,
                bobInput2,
                charlieInput1
            };
            foreach (var input in inputs)
            {
                subject.AcceptInput(input);
            }

            return subject;
        }

        private InputHandler GetSubject()
        {
            var subject = new InputHandler(GetDateDiffProvider());
            return subject;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AcceptInput_AlicePosts_NilOutput()
        {
            var input = aliceInput1;
            var subject = GetSubject();
            var actual = subject.AcceptInput(input);
            Assert.AreEqual(0, actual.Count);

        }

        [Test]
        public void AcceptInput_AllPosts_NilOutput()
        {
            var subject = GetSubject();
            var inputs = new List<string>
            {
                aliceInput1,
                bobInput1,
                bobInput2,
                charlieInput1
            };
            foreach (var input in inputs)
            {
                var actual = subject.AcceptInput(input);
                Assert.AreEqual(0, actual.Count);
            }

        }
        [Test]
        public void ReadAlice_AllPostsPosted_ReturnsAlicesMessagesFormatted()
        {
            var subject = GetSubjectWithMessagesPosted();
            var actual = subject.AcceptInput("Alice");
            Assert.AreEqual(1,actual.Count);
            Assert.AreEqual("I love the weather today (5 minutes ago)", actual[0]);
        }

        [Test]
        public void ReadBob_AllPostsPosted_ReturnsBobsMessagesFormatted()
        {
            var subject = GetSubjectWithMessagesPosted();
            var actual = subject.AcceptInput("Bob");
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual("Good game though. (1 minute ago)", actual[0]);
            Assert.AreEqual("Damn! We lost! (2 minutes ago)", actual[1]);
        }

        /*> Charlie follows Alice
> Charlie wall
> Charlie - I'm in New York today! Anyone wants to have a coffee? (2 seconds ago)
> Alice - I love the weather today (5 minutes ago)*/

        [Test]
        public void CharlieFollowsAliceThenCharlieWallThenFollowsBobThenWall_AllPostsPosted_ReturnsWallFormatted()
        {
            var subject = GetSubjectWithMessagesPosted();

            var actual = subject.AcceptInput("Charlie follows Alice");
            Assert.AreEqual(0, actual.Count);

            actual = subject.AcceptInput("Charlie wall");
            Assert.AreEqual(2, actual.Count);
            Assert.AreEqual("Charlie - I'm in New York today! Anyone wants to have a coffee? (2 seconds ago)", actual[0]);
            Assert.AreEqual("Alice - I love the weather today (5 minutes ago)", actual[1]);

            actual = subject.AcceptInput("Charlie follows Bob");
            Assert.AreEqual(0, actual.Count);
            actual = subject.AcceptInput("Charlie wall");
            Assert.AreEqual(4, actual.Count);

            var expectedOutputs = new List<string>
            {
                "Charlie - I'm in New York today! Anyone wants to have a coffee? (2 seconds ago)",
                "Bob - Good game though. (1 minute ago)",
                "Bob - Damn! We lost! (2 minutes ago)",
                "Alice - I love the weather today (5 minutes ago)"
            };
            for (int i = 0; i < expectedOutputs.Count; i++)
            {
                Assert.AreEqual(expectedOutputs[i], actual[i]);
            }
        }
    }

}