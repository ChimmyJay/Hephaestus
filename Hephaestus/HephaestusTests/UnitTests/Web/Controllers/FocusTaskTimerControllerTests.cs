using System;
using System.Collections.Generic;
using System.Linq;
using HephaestusDomain.Models;
using HephaestusDomain.Services;
using HephaestusWeb.Controllers;
using HephaestusWeb.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace HephaestusTests.UnitTests.Web.Controllers
{
    [TestFixture]
    public class FocusTaskTimerControllerTests
    {
        private IFocusTaskTimerService _fakeFocusTaskTimerService;
        private FocusTaskTimerController _target;
        private IActionResult _actual;

        [SetUp]
        public void SetUp()
        {
            _fakeFocusTaskTimerService = Substitute.For<IFocusTaskTimerService>();
            _target = new FocusTaskTimerController(_fakeFocusTaskTimerService);
        }

        [Test]
        public void GetFocusingTask_HasFocusTask_should_be_true_when_has_focusing_task()
        {
            GivenFocusingTask(new FocusTask());
            HasFocusTaskShouldBe(true);
        }

        [Test]
        public void GetFocusingTask_verify_task_info_mapping_when_has_focusing_task()
        {
            var focusingTask = new FocusTask
            {
                Name = "Task1",
                StartTime = new DateTime(2021, 1, 1)
            };
            GivenFocusingTask(focusingTask);
            TaskInfoShouldMapping(focusingTask);
        }

        [Test]
        public void GetFocusingTask_HasFocusTask_should_be_false_when_not_has_focusing_task()
        {
            NotGivenFocusingTask();
            HasFocusTaskShouldBe(false);
        }

        [Test]
        public void StartFocusingTask()
        {
            var request = new StartFocusingTaskRequest
            {
                Name = "test",
                StartTime = new DateTime(2020, 01, 30)
            };

            WhenServiceStartFocusingTask(request);

            _fakeFocusTaskTimerService
                .Received(1)
                .StartFocusingTask(Arg.Is<StartFocusingTaskDto>(x =>
                    x.Name == request.Name
                    && x.StartTime == request.StartTime));
            HttpStatusCodeShouldBe(200);
        }

        [Test]
        public void StopFocusingTask()
        {
            var endTime = DateTime.Now;
            _actual = _target.StopFocusingTask(new StopFocusingTaskRequest()
            {
                EndTime = endTime
            });

            _fakeFocusTaskTimerService
                .Received(1)
                .StopFocusingTask(endTime);
            HttpStatusCodeShouldBe(200);
        }

        [Test]
        public void GetFocusTaskHistory()
        {
            _fakeFocusTaskTimerService.GetFocusTaskHistory()
                .Returns(new List<FocusTask>
                {
                    new FocusTask()
                    {
                        Name = "Test1",
                        StartTime = new DateTime(2021, 01, 01, 01, 00, 00),
                        EndTime = new DateTime(2021, 01, 01, 01, 00, 20)
                    }
                });

            var jsonResult = (JsonResult)_target.GetFocusTaskHistory();

            var data = (List<FocusTaskHistoryViewModel>)jsonResult.Value;
            Assert.AreEqual("Test1", data.Single().Name);
            Assert.AreEqual(new DateTime(2021, 01, 01, 01, 00, 00), data.Single().StartTime);
            Assert.AreEqual(new DateTime(2021, 01, 01, 01, 00, 20), data.Single().EndTime);
            Assert.AreEqual(20, data.Single().ElapsedTime);
        }

        private void WhenServiceStartFocusingTask(StartFocusingTaskRequest request)
        {
            _actual = _target.StartFocusingTask(request);
        }

        private void HttpStatusCodeShouldBe(int expected)
        {
            var statusCodeResult = (StatusCodeResult)_actual;
            Assert.AreEqual(expected, statusCodeResult.StatusCode);
        }

        private static void HttpStatusCodeShouldBe(int expected, StatusCodeResult actual)
        {
            Assert.AreEqual(expected, actual.StatusCode);
        }

        private void TaskInfoShouldMapping(FocusTask focusTask)
        {
            var jsonResult = (JsonResult)_target.GetFocusingTask();
            var data = (FocusTaskViewModel)jsonResult.Value;
            Assert.AreEqual(focusTask.Name, data.TaskName);
            Assert.AreEqual(focusTask.StartTime, data.TaskStartTime);
        }

        private void NotGivenFocusingTask()
        {
            _fakeFocusTaskTimerService.GetFocusingTask().ReturnsNull();
        }

        private void HasFocusTaskShouldBe(bool expected)
        {
            var jsonResult = (JsonResult)_target.GetFocusingTask();
            var data = (FocusTaskViewModel)jsonResult.Value;
            Assert.AreEqual(expected, data.HasFocusTask);
        }

        private void GivenFocusingTask(FocusTask focusTask)
        {
            _fakeFocusTaskTimerService.GetFocusingTask().Returns(focusTask);
        }
    }
}