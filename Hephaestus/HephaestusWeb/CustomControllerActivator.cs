using HephaestusDomain.Services;
using HephaestusSQLiteRepo;
using HephaestusSQLiteRepo.Repos;
using HephaestusWeb.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;

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
            var disposables =
                (List<IDisposable>)context.HttpContext
                    .Items["Disposables"];

            if (disposables != null)
            {
                disposables.Reverse();
                foreach (var disposable in disposables)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}