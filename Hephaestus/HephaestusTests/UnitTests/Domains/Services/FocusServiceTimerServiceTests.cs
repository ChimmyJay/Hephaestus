using HephaestusDomain.Models;
using HephaestusDomain.Repos;
using HephaestusDomain.Services;
using NSubstitute;
using NUnit.Framework;
using System;

namespace HephaestusTests.UnitTests.Domains.Services
{
    [TestFixture]
    public class FocusServiceTimerServiceTests
    {
        private IFocusTaskRepo _fakeRepo;
        private FocusTaskTimerService _target;

        [SetUp]
        public void SetUp()
        {
            _fakeRepo = Substitute.For<IFocusTaskRepo>();
            _target = new FocusTaskTimerService(_fakeRepo);
        }

        [Test]
        public void get_focusing_task()
        {
            var focusTask = new FocusTask
            {
                Name = "TestTask",
                StartTime = new DateTime(2021, 01, 28)
            };
            GivenToRepo(focusTask);
            TaskInfoShouldMapping(focusTask);
            GetFromRepoShouldBeCall(1);
        }

        private void GivenToRepo(FocusTask focusTask)
        {
            _fakeRepo.Get().Returns(focusTask);
        }

        private void TaskInfoShouldMapping(FocusTask focusTask)
        {
            var actual = _target.GetFocusingTask();
            Assert.AreEqual((focusTask).Name, actual.Name);
            Assert.AreEqual((focusTask).StartTime, actual.StartTime);
        }

        private void GetFromRepoShouldBeCall(int times)
        {
            _fakeRepo.Received(times).Get();
        }
    }
}