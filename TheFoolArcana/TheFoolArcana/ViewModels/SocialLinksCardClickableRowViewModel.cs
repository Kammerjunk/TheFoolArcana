using System.Collections.Generic;
using TheFoolArcana.Models;

namespace TheFoolArcana.ViewModels
{
    public class SocialLinksCardClickableRowViewModel
    {
        public IEnumerable<string> Cards { get; set; }

        public SocialLinksCardClickableRowViewModel(IEnumerable<string> cards)
        {
            Cards = cards;
        }
    }
}
