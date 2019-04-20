using System;
using Moq;
using NUnit.Framework;
using Wall01;

namespace Tests
{
    [TestFixture]
    public class UsersRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetNextId_EmptyRepo_Returns0()
        {
            var subject = new UsersRepository();
            var actual = subject.GetNextId();
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNextId_1User1Post_Returns1()
        {
            var subject = new UsersRepository();
            subject.Post("u1", "t1");
            var actual = subject.GetNextId();
            var expected = 1;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetNextId_1User2Post_Returns2()
        {
            var subject = new UsersRepository();
            subject.Post("u1", "t1");
            subject.Post("u1", "t1");
            var actual = subject.GetNextId();
            var expected = 2;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetNextId_2User2Post_Returns2()
        {
            var subject = new UsersRepository();
            subject.Post("u1", "t1");
            subject.Post("u2", "t1");
            var actual = subject.GetNextId();
            var expected = 2;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetNextId_3User3Post_Returns3()
        {
            var subject = new UsersRepository();
            subject.Post("u1", "t1");
            subject.Post("u2", "t1");
            subject.Post("u3", "t1");
            var actual = subject.GetNextId();
            var expected = 3;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetNextId_3User5Post_Returns5()
        {
            var subject = new UsersRepository();
            subject.Post("u1", "t0");
            subject.Post("u2", "t0");
            subject.Post("u3", "t0");
            subject.Post("u2", "t1");
            subject.Post("u3", "t1");
            var actual = subject.GetNextId();
            var expected = 5;
            Assert.AreEqual(expected, actual);
        }
    }

}