using HephaestusDomain.Models;
using HephaestusDomain.Repos;
using HephaestusDomain.Services;
using HephaestusWeb.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;

namespace HephaestusWeb
{
    public class CustomControllerActivator : IControllerActivator
    {
        public object Create(ControllerContext context)
        {
            var type = context.ActionDescriptor.ControllerTypeInfo.AsType();
            if (type == typeof(FocusTaskTimerController))
            {
                return new FocusTaskTimerController(new FocusTaskTimerService(new MemoryRepo()));
            }
            throw new Exception($"Unknown controller {type.Name}");
        }

        public void Release(ControllerContext context, object controller)
        {
        }
    }

    public class MemoryRepo : IFocusTaskRepo
    {
        public FocusTask Get()
        {
            return new FocusTask()
            {
                Name = "TestPureDI",
                StartTime = new DateTime(2021, 01, 29)
            };
        }
    }
}