using System;
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

        [SetUp]
        public void SetUp()
        {
            _fakeFocusTaskTimerService = Substitute.For<IFocusTaskTimerService>();
            _target = new FocusTaskTimerController(_fakeFocusTaskTimerService);
        }

        [Test]
        public void HasFocusTask_should_be_true_when_has_focusing_task()
        {
            GivenFocusingTask(new FocusTask());
            HasFocusTaskShouldBe(true);
        }

        [Test]
        public void verify_task_info_mapping_when_has_focusing_task()
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
        public void HasFocusTask_should_be_false_when_not_has_focusing_task()
        {
            NotGivenFocusingTask();
            HasFocusTaskShouldBe(false);
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