using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheFoolArcana.ViewModels;

namespace TheFoolArcana.Controllers
{
    public class SocialLinksController : Controller
    {
        public IActionResult Index()
        {

            var vm = new SocialLinksIndexViewModel(5); //TODO config
            return View(vm);
        }
    }
}