using System;
using System.Collections.Generic;
using System.Linq;
using HephaestusDomain.Models;
using HephaestusDomain.Repos;
using HephaestusDomain.Services;
using NSubstitute;
using NUnit.Framework;

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
            var endTime = DateTime.Now;
            _target.StopFocusingTask(endTime);
            _fakeRepo.Received(1).StopFocusing(endTime);
        }

        [Test]
        public void GetFocusTaskHistory_should_call_repo_GetHistory_once()
        {
            var actual = _target.GetFocusTaskHistory();
            _fakeRepo.Received(1).GetHistory();
        }

        [Test]
        public void GetFocusTaskHistory_calculate_ElapsedTime()
        {
            _fakeRepo.GetHistory().Returns(new List<FocusTask>
            {
                new FocusTask()
                {
                    Name = "Test1",
                    StartTime = new DateTime(2021,01,01).AddSeconds(-20),
                    EndTime = new DateTime(2021,01,01)
                }
            });

            var actual = _target.GetFocusTaskHistory();

            Assert.AreEqual(20, actual.Single().ElapsedTime);
        }

        [Test]
        public void GetFocusTaskHistory_show_all_history()
        {
            _fakeRepo.GetHistory().Returns(new List<FocusTask>
            {
                new FocusTask { EndTime = new DateTime(2020,01,01) },
                new FocusTask { EndTime = new DateTime(2021,01,01) },
                new FocusTask { EndTime = new DateTime(2022,01,01) },
            });

            var actual = _target.GetFocusTaskHistory();
            Assert.AreEqual(3, actual.Count());
        }

        [Test]
        public void GetFocusTaskHistory_should_sort_by_endTime_descending()
        {
            _fakeRepo.GetHistory()
                .Returns(new List<FocusTask>
                {
                    new FocusTask {EndTime = new DateTime(2021, 01, 01, 01, 00, 01)},
                    new FocusTask {EndTime = new DateTime(2021, 01, 01, 01, 00, 02)},
                    new FocusTask {EndTime = new DateTime(2021, 01, 01, 01, 00, 00)}
                });
            var actual = _target.GetFocusTaskHistory();
            Assert.That(actual, Is.Ordered.Descending.By(nameof(FocusTask.EndTime)));
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