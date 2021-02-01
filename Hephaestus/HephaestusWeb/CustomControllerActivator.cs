using HephaestusDomain.Services;
using HephaestusSQLiteRepo;
using HephaestusWeb.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using HephaestusSQLiteRepo.Repos;

namespace HephaestusWeb
{
    public class CustomControllerActivator : IControllerActivator
    {
        private readonly string _dataSourceConnectionString;

        public CustomControllerActivator(string dataSourceConnectionString)
        {
            _dataSourceConnectionString = dataSourceConnectionString;
        }

        public object Create(ControllerContext context)
        {
            var type = context.ActionDescriptor.ControllerTypeInfo.AsType();
            if (type == typeof(FocusTaskTimerController))
            {
                return new FocusTaskTimerController(
                    new FocusTaskTimerService(
                        new FocusTaskRepo(
                            new SQLiteContext(_dataSourceConnectionString))));
            }
            throw new Exception($"Unknown controller {type.Name}");
        }

        public void Release(ControllerContext context, object controller)
        {
        }
    }
}