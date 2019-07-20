using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEF.Providers;
using MEF.Providers.Abstract;
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
            ViewModel model = new ViewModel
            {
                LogControl = _logProvider.LogControl(),
                LogTest = _logProvider.LogAll("Message")
            };
            return View(model);
        }

        public class ViewModel
        {
            public IEnumerable<(string Id,string Value)> LogControl { get; set; }
            public IEnumerable<string> LogTest { get; set; }
        }
    }
}