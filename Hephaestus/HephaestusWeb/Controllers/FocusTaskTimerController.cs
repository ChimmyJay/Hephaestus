using HephaestusWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HephaestusWeb.Controllers
{
    public class FocusTaskTimerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetFocusingTask()
        {
            return Json(new FocusTaskViewModel
            {
                HasFocusTask = true,
                TaskName = "TestFocusingTask",
                TaskStartTime = DateTime.Now.AddMinutes(-2)
            });
        }
    }
}