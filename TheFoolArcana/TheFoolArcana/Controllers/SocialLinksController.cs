using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TheFoolArcana.Models.Options;
using TheFoolArcana.ViewModels;

namespace TheFoolArcana.Controllers
{
    public class SocialLinksController : Controller
    {
        private SocialLinksOptions Options { get; }

        public SocialLinksController(IConfiguration config)
        {
            Options = new SocialLinksOptions();
            config.GetSection(SocialLinksOptions.SocialLinks).Bind(Options);
        }

        public IActionResult Index()
        {

            var vm = new SocialLinksIndexViewModel(Options.MaxCardsPerRow); //TODO config
            return View(vm);
        }
    }
}