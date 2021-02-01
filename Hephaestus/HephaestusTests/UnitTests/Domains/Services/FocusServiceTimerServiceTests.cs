using HephaestusDomain.Models;
using HephaestusDomain.Repos;
using HephaestusDomain.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public void GetFocusingTask()
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

        [Test]
        public void StartFocusingTask()
        {
            var startFocusingTaskDto = new StartFocusingTaskDto();

            _target.StartFocusingTask(startFocusingTaskDto);

            _fakeRepo.Received(1).StartFocusing(startFocusingTaskDto);
        }

        [Test]
        public void StopFocusingTask()
        {
            _target.StopFocusingTask();
            _fakeRepo.Received(1).Clear();
        }

        [Test]
        public void GetFocusTaskHistory()
        {
            _fakeRepo.GetHistory().Returns(new List<FocusTask>
            {
                new FocusTask()
                {
                    Name = "Test1",
                    StartTime = new DateTime(2021, 01, 01, 01, 00, 00),
                    EndTime = new DateTime(2021, 01, 01, 01, 00, 20),
                }
            });
            var actual = _target.GetFocusTaskHistory();
            Assert.AreEqual(20, actual.Single().ElapsedTime);
            _fakeRepo.Received(1).GetHistory();
        }

        private void GivenToRepo(FocusTask focusTask)
        {
            _fakeRepo.GetFocusing().Returns(focusTask);
        }

        private void TaskInfoShouldMapping(FocusTask focusTask)
        {
            var actual = _target.GetFocusingTask();
            Assert.AreEqual((focusTask).Name, actual.Name);
            Assert.AreEqual((focusTask).StartTime, actual.StartTime);
        }

        private void GetFromRepoShouldBeCall(int times)
        {
            _fakeRepo.Received(times).GetFocusing();
        }
    }
}