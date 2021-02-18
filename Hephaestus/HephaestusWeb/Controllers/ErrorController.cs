using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HephaestusWeb.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}