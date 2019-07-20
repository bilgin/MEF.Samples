using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEF.Providers;
using Microsoft.AspNetCore.Mvc;

namespace MEF.Web.Controllers
{
    public class HomeController : Controller
    {
        private ILogProvider _logProvider;
        public HomeController(ILogProvider logProvider)
        {
            _logProvider = logProvider;
        }

        public IActionResult Index()
        {
            return View(_logProvider.Log("Message"));
        }
    }
}