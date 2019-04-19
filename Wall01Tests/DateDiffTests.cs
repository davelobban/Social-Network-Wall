using System;
using Moq;
using NUnit.Framework;
using Wall01;

namespace Tests
{
    [TestFixture]
    public class DateDiffTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DateTimeSubtract_5Mins_Returns5Minutes()
        {
            var t1 = new DateTime(2000, 01, 01, 12, 00, 00);
            var t2 = new DateTime(2000, 01, 01, 12, 05, 00);
            var actual = t2.Subtract(t1);
            var expected = new TimeSpan(0, 0, 5, 0);
            Assert.AreEqual(expected, actual);
        }


        //sec
        [Test]
        public void GetTimeBetween_1Sec_Returns1Second()
        {
            var t1 = new DateTime(2000, 01, 01, 12, 00, 00);
            var t2 = new DateTime(2000, 01, 01, 12, 00, 01);

            var subject = new DateDiff();
            var actual = subject.GetTimeBetween(t1, t2);
            var expected = "(1 second ago)";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetTimeBetween_2Secs_Returns2Seconds()
        {
            var t1 = new DateTime(2000, 01, 01, 12, 00, 00);
            var t2 = new DateTime(2000, 01, 01, 12, 00, 02);

            var subject = new DateDiff();
            var actual = subject.GetTimeBetween(t1, t2);
            var expected = "(2 seconds ago)";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTimeBetween_59Secs_Returns59Seconds()
        {
            var t1 = new DateTime(2000, 01, 01, 12, 00, 00);
            var t2 = new DateTime(2000, 01, 01, 12, 00, 59);

            var subject = new DateDiff();
            var actual = subject.GetTimeBetween(t1, t2);
            var expected = "(59 seconds ago)";
            Assert.AreEqual(expected, actual);
        }
        //sec


        [Test]
        public void GetTimeBetween_1Min_Returns1Minute()
        {
            var t1 = new DateTime(2000, 01, 01, 12, 00, 00);
            var t2 = new DateTime(2000, 01, 01, 12, 01, 00);

            var subject = new DateDiff();
            var actual = subject.GetTimeBetween(t1, t2);
            var expected = "(1 minute ago)";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTimeBetween_5Mins_Returns5Minutes()
        {
            var t1 = new DateTime(2000, 01, 01, 12, 00, 00);
            var t2 = new DateTime(2000, 01, 01, 12, 05, 00);

            var subject = new DateDiff();
            var actual = subject.GetTimeBetween(t1, t2);
            var expected = "(5 minutes ago)";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetTimeBetween_6Mins_Returns6Minutes()
        {
            var t1 = new DateTime(2000, 01, 01, 12, 00, 00);
            var t2 = new DateTime(2000, 01, 01, 12, 06, 00);

            var subject = new DateDiff();
            var actual = subject.GetTimeBetween(t1, t2);
            var expected = "(6 minutes ago)";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTimeBetween_59Mins_Returns59Minutes()
        {
            var t1 = new DateTime(2000, 01, 01, 12, 00, 00);
            var t2 = new DateTime(2000, 01, 01, 12, 59, 00);

            var subject = new DateDiff();
            var actual = subject.GetTimeBetween(t1, t2);
            var expected = "(59 minutes ago)";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetTimeBetween60Mins_Returns1Hour()
        {
            var t1 = new DateTime(2000, 01, 01, 12, 00, 00);
            var t2 = new DateTime(2000, 01, 01, 13, 0, 00);

            var subject = new DateDiff();
            var actual = subject.GetTimeBetween(t1, t2);
            var expected = "(1 hour ago)";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetTimeBetween61Mins_Returns1Hour()
        {
            var t1 = new DateTime(2000, 01, 01, 12, 00, 00);
            var t2 = new DateTime(2000, 01, 01, 13, 1, 00);

            var subject = new DateDiff();
            var actual = subject.GetTimeBetween(t1, t2);
            var expected = "(1 hour ago)";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetTimeBetween119Mins_Returns1Hour()
        {
            var t1 = new DateTime(2000, 01, 01, 12, 00, 00);
            var t2 = new DateTime(2000, 01, 01, 13, 59, 00);

            var subject = new DateDiff();
            var actual = subject.GetTimeBetween(t1, t2);
            var expected = "(1 hour ago)";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetTimeBetween120Mins_Returns2Hour()
        {
            var t1 = new DateTime(2000, 01, 01, 12, 00, 00);
            var t2 = new DateTime(2000, 01, 01, 14, 0, 00);

            var subject = new DateDiff();
            var actual = subject.GetTimeBetween(t1, t2);
            var expected = "(2 hours ago)";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetTimeBetween23H59Mins_Returns23Hour()
        {
            var t1 = new DateTime(2000, 01, 01, 12, 00, 00);
            var t2 = new DateTime(2000, 01, 02, 11, 59, 59);

            var subject = new DateDiff();
            var actual = subject.GetTimeBetween(t1, t2);
            var expected = "(23 hours ago)";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetTimeBetween1Day_ReturnsMoreThan24Hours()
        {
            var t1 = new DateTime(2000, 01, 01, 12, 00, 00);
            var t2 = new DateTime(2000, 01, 02, 12, 00, 00);

            var subject = new DateDiff();
            var actual = subject.GetTimeBetween(t1, t2);
            var expected = "(more than 24 hours ago)";
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetTimeBetween1Year_ReturnsMoreThan24Hours()
        {
            var t1 = new DateTime(2000, 01, 01, 12, 00, 00);
            var t2 = new DateTime(2001, 01, 01, 12, 00, 00);

            var subject = new DateDiff();
            var actual = subject.GetTimeBetween(t1, t2);
            var expected = "(more than 24 hours ago)";
            Assert.AreEqual(expected, actual);
        }
    }


}