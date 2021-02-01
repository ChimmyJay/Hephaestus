using HephaestusDomain.Models;
using HephaestusDomain.Services;
using HephaestusWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        public IActionResult StopFocusingTask([FromBody] StopFocusingTaskRequest stopFocusingTaskRequest)
        {
            _focusTaskTimerService.StopFocusingTask(stopFocusingTaskRequest.EndTime);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetFocusTaskHistory()
        {
            var data = _focusTaskTimerService.GetFocusTaskHistory()
                .Select(x => new FocusTaskHistoryViewModel
                {
                    Name = x.Name,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    ElapsedTime = x.ElapsedTime
                }).ToList();
            return Json(data);
        }
    }
}