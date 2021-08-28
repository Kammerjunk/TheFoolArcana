using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheFoolArcana.ViewModels
{
    public class SocialLinksIndexViewModel
    {
        public int MaxCardsPerRow { get; set; }

        public SocialLinksIndexViewModel(int maxCardsPerRow)
        {
            MaxCardsPerRow = maxCardsPerRow;
        }
    }
}
