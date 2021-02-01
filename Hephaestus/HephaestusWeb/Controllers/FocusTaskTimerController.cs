using HephaestusDomain.Models;
using HephaestusDomain.Services;
using HephaestusWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HephaestusWeb.Controllers
{
    public class FocusTaskTimerController : Controller
    {
        private readonly IFocusTaskTimerService _focusTaskTimerService;

        public FocusTaskTimerController(IFocusTaskTimerService focusTaskTimerService)
        {
            _focusTaskTimerService = focusTaskTimerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetFocusingTask()
        {
            var focusingTask = _focusTaskTimerService.GetFocusingTask();
            return Json(new FocusTaskViewModel
            {
                HasFocusTask = focusingTask != null,
                TaskName = focusingTask?.Name,
                TaskStartTime = focusingTask?.StartTime
            });
        }

        [HttpPost]
        public IActionResult StartFocusingTask([FromBody] StartFocusingTaskRequest request)
        {
            var dto = new StartFocusingTaskDto
            {
                Name = request.Name,
                StartTime = request.StartTime
            };
            _focusTaskTimerService.StartFocusingTask(dto);
            return Ok();
        }

        [HttpPost]
        public IActionResult StopFocusingTask()
        {
            _focusTaskTimerService.StopFocusingTask();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetFocusTaskHistory()
        {
            var focusTaskHistoryViewModels = new List<FocusTaskHistoryViewModel>
            {
                new FocusTaskHistoryViewModel()
                {
                    Name = "Test1",
                    StartTime = DateTime.Now.AddMinutes(-5).ToString(),
                    EndTime = DateTime.Now.AddMinutes(-3).ToString(),
                    ElapsedTime = "00:02:00"
                },
                new FocusTaskHistoryViewModel()
                {
                    Name = "Test2",
                    StartTime = DateTime.Now.AddMinutes(-2).ToString(),
                    EndTime = DateTime.Now.AddMinutes(-1).ToString(),
                    ElapsedTime = "00:01:00"
                }
            };
            return Json(focusTaskHistoryViewModels);
        }
    }
}