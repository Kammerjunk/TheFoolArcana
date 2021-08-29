using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TheFoolArcana.DbService;
using TheFoolArcana.Models;
using TheFoolArcana.Models.DbService;
using TheFoolArcana.Models.Options;
using TheFoolArcana.ViewModels;

namespace TheFoolArcana.Controllers
{
    public class SocialLinksController : Controller
    {
        private DbFacade Db { get; }
        private SocialLinksOptions Options { get; }

        public SocialLinksController(IConfiguration config)
        {
            Db = new DbFacade(config);

            Options = new SocialLinksOptions();
            config.GetSection(SocialLinksOptions.SocialLinks).Bind(Options);
        }

        public IActionResult Index()
        {
            DbResponseAllRows<SocialLink> socialLinks = Db.AllRows<SocialLink>("GetSocialLinks");

            IEnumerable<string> arcanas = socialLinks.Output
                .Select(sl => sl.Arcana)
                .Distinct();

            var vm = new SocialLinksIndexViewModel(Options.MaxCardsPerRow, arcanas);
            return View(vm);
        }

        public IActionResult Details(string id)
        {
            return View();
        }
    }
}