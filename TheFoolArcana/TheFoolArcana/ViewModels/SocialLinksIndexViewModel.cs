using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheFoolArcana.Models;

namespace TheFoolArcana.ViewModels
{
    public class SocialLinksIndexViewModel
    {
        public int MaxCardsPerRow { get; set; }
        public IEnumerable<string> Arcanas { get; set; }

        public SocialLinksIndexViewModel(int maxCardsPerRow, IEnumerable<string> arcanas)
        {
            MaxCardsPerRow = maxCardsPerRow;
            Arcanas = arcanas;
        }
    }
}
